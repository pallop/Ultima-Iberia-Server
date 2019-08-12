namespace Server.Items
{
    #region Usings

    using System.Text.RegularExpressions;
    using Server.Mobiles;

    #endregion

    /// <summary>
    /// Base class for all wearable items. Extends functionality by adding doubleclick equipping/unequipping/swapping 
    /// as well as ability to equip any item to any mobile.
    /// </summary>
    public class WearableItem : Item
    {
        #region Constructors and Destructors

        public WearableItem(int itemId)
            : base(itemId)
        {
        }

        public WearableItem(Serial serial)
            : base(serial)
        {
        }

        #endregion

        private enum Operation
        {
            SwapHands,
            Swap3,
            Swap,
            Equip
        }

        #region Public Methods

        public override string DefaultName
        {
            // Server is not aware of default names for OSI items as its using and passing only cliloc numbers, so lets extract it from item type
            // NOTE: From now on, wearables will no longer have null name, can this possibly be a bad thing?
            get
            {
                var itemFullType = this.GetType().ToString();
                var itemType = itemFullType.Remove(0, itemFullType.LastIndexOf('.') + 1);
                return Regex.Replace(itemType, @"([A-Z])(?<=[a-z]\1|[A-Za-z]\1(?=[a-z]))", " $1").Trim();
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.Parent == from && this.Movable)
            {
                // Player dclicked item on their paperdoll so we just put it into their backpack and accompany it with cool message.
                if (from.Backpack.TryDropItem(from, this, true))
                {
                    var typeString = this is BaseWeapon ? "sheated" : this is BaseClothing ? "folded" : "stored";
                    from.SendMessage(this.Hue, "You {0} {1} into your backpack.", typeString, this.Name);
                }

                return;
            }

            if (!this.IsAccessibleTo(from) || this.Layer == Layer.Invalid || this.Parent is Corpse || from.Backpack == null || !this.CanEquip(from)
                || !this.Movable)
            {
                // Lets not do something crazy like equipping unequippable/unreachable item
                // Also we don't want corpses to be handled in any way as players could possibly swap their rare item for some junk in there
                return;
            }

            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(500295); // You are too far away to do that.
                return;
            }

            this.EquipTo(from);
        }

        public void EquipTo(Mobile mobile, bool? deleteConflictOverride = null)
        {
            var itemOne = mobile.FindItemOnLayer(Layer.OneHanded);
            var itemTwo = mobile.FindItemOnLayer(Layer.TwoHanded);
            var operation = Operation.Equip;
            var container = this.Parent as Container ?? mobile.Backpack;

            // If target mobile is NPC we delete conflicting item, if player we drop it into their backpack (unless deleteConflictOverride is set)
            var deleteConflict = deleteConflictOverride ?? !(mobile is PlayerMobile);

            if (this.Layer != Layer.OneHanded && this.Layer != Layer.TwoHanded)
            {
                // Item layer is of a simple handling 
                itemOne = mobile.FindItemOnLayer(this.Layer);
                if (itemOne != null)
                {
                    operation = Operation.Swap;
                }
            }
            else
            {
                // Handling weapons and shields gets a bit complicated as there are several possible combinations

                if (this.Layer == Layer.TwoHanded)
                {
                    // Trying to equip either two-handed weapon or shield
                    if (this is BaseShield)
                    {
                        // Its a shield!
                        if (itemTwo != null)
                        {
                            operation = Operation.SwapHands;
                        }
                    }
                    else
                    {
                        // Its a two-handed weapon!
                        if (itemTwo != null)
                        {
                            if (itemOne != null)
                            {
                                // Mobile has one handed weapon and shield
                                operation = Operation.Swap3;
                            }
                            else if (itemOne == null)
                            {
                                // Mobile has just two-handed weapon
                                operation = Operation.SwapHands;
                            }
                        }
                        else if (itemOne != null)
                        {
                            // Mobile has just one-handed weapon
                            operation = Operation.SwapHands;
                        }
                    }
                }
                else
                {
                    // Trying to equip one-handed weapon
                    if (itemOne != null)
                    {
                        // Mobile has one-handed weapon
                        if (itemTwo != null && itemTwo is BaseShield)
                        {
                            // Mobile has one-handed weapon and shield (preserve shield)
                            itemTwo = null;
                        }

                        operation = Operation.SwapHands;
                    }
                    else if (itemTwo != null && !(itemTwo is BaseShield))
                    {
                        // Mobile has two-handed weapon
                        operation = Operation.SwapHands;
                    }
                }
            }

            var layerToSwap = itemOne;

            switch (operation)
            {
                case Operation.SwapHands:
                    layerToSwap = itemTwo ?? itemOne;
                    goto case Operation.Swap;
                case Operation.Swap:
                    if (!deleteConflict)
                    {
                        if (container.TryDropItem(mobile, layerToSwap, mobile is PlayerMobile))
                        {
                            if (mobile.EquipItem(this) && mobile is PlayerMobile)
                            {
                                mobile.SendMessage(this.Hue, "You swapped {0} with {1}.", layerToSwap.Name, this.Name);
                                layerToSwap.Location = this.Location;
                            }
                        }
                    }
                    else
                    {
                        layerToSwap.Delete();
                        goto default;
                    }
                    break;
                case Operation.Swap3:
                    if (!deleteConflict)
                    {
                        if (container.TryDropItem(mobile, itemOne, mobile is PlayerMobile) && container.TryDropItem(mobile, itemTwo, mobile is PlayerMobile))
                        {
                            if (mobile.EquipItem(this) && mobile is PlayerMobile)
                            {
                                mobile.SendMessage(this.Hue, "You swapped {0} and {1} with {2}.", itemOne.Name, itemTwo.Name, this.Name);
                                itemOne.Location = this.Location;
                                itemTwo.Location = this.Location;
                            }
                        }
                    }
                    else
                    {
                        itemOne.Delete();
                        itemTwo.Delete();
                        goto default;
                    }
                    break;
                default:
                    if (mobile.EquipItem(this) && mobile is PlayerMobile)
                    {
                        var typeString = this is BaseWeapon ? "unsheated" : this is BaseClothing ? "dressed into" : "equipped";
                        mobile.SendMessage(this.Hue, "You {0} {1}.", typeString, this.Name);
                    }

                    break;
            }
        }

        #region Serialization

        // ReSharper disable RedundantOverridenMember
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }

        // ReSharper restore RedundantOverridenMember
        #endregion

        #endregion
    }
}