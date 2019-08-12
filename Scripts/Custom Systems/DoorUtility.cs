#region Header
//   Vorspire    _,-'/-'/  DoorUtility.cs
//   .      __,-; ,'( '/
//    \.    `-.__`-._`:_,-._       _ , . ``
//     `:-._,------' ` _,`--` -: `_ , ` ,' :
//        `---..__,,--'  (C) 2017  ` -'. -'
//        #  Vita-Nex [http://core.vita-nex.com]  #
//  {o)xxx|===============-   #   -===============|xxx(o}
//        #        The MIT License (MIT)          #
#endregion

#region References
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

using Server.Commands;
using Server.Items;
using Server.Multis;
#endregion

//#define STRICT

namespace Server
{
	public static class DoorUtility
	{
		public static string DoorsPath = "Data/Decoration/Doors.xml";

		public static void Initialize()
		{
			CommandSystem.Register("DoorExport", AccessLevel.Administrator, OnExportDoors);
			CommandSystem.Register("DoorImport", AccessLevel.Administrator, OnImportDoors);
		}

		[Usage("DoorExport <path:optional>]")]
		[Description("Exports doors in all static guarded regions to an Xml file.")]
		private static void OnExportDoors(CommandEventArgs e)
		{
			var total = 0;
			var hue = 0x55;
			var sw = Stopwatch.StartNew();

			try
			{
				var path = e.GetString(0);

				if (String.IsNullOrWhiteSpace(path))
				{
					path = DoorsPath;
				}

				var result = ExportDoors(path);

				foreach (var o in result)
				{
					e.Mobile.SendMessage(hue, "{0}: {1:#,0} doors exported.", o.Key.Name, o.Value);

					total += o.Value;
				}

				result.Clear();
			}
			catch (Exception x)
			{
				hue = 0x22;

				try
				{
					File.AppendAllLines("DoorExport.log", new[] {String.Empty, DateTime.Now.ToString(), x.ToString(), String.Empty});

					e.Mobile.SendMessage(hue, "Door export failed with {0}. See DoorExport.log", x.GetType().Name);
				}
				catch
				{ }
			}
			finally
			{
				sw.Stop();

				if (total > 0)
				{
					e.Mobile.SendMessage(hue, "{0:#,0} doors have been exported in total.", total);
				}
				else
				{
					e.Mobile.SendMessage(hue, "No doors have been exported.");
				}

				e.Mobile.SendMessage(hue, "Door export completed in {0:F2} seconds.", sw.Elapsed.TotalSeconds);
			}
		}

		[Usage("DoorImport <path:optional>]")]
		[Description("Imports doors from an Xml file and adds them to the world.")]
		private static void OnImportDoors(CommandEventArgs e)
		{
			var total = 0;
			var hue = 0x55;
			var sw = Stopwatch.StartNew();

			try
			{
				var path = e.GetString(0);

				if (String.IsNullOrWhiteSpace(path))
				{
					path = DoorsPath;
				}

				var result = ImportDoors(path);

				foreach (var o in result)
				{
					e.Mobile.SendMessage(hue, "{0}: {1:#,0} doors imported.", o.Key.Name, o.Value);

					total += o.Value;
				}

				result.Clear();
			}
			catch (Exception x)
			{
				hue = 0x22;

				try
				{
					File.AppendAllLines("DoorImport.log", new[] {String.Empty, DateTime.Now.ToString(), x.ToString(), String.Empty});

					e.Mobile.SendMessage(hue, "Door import failed with {0}. See DoorImport.log", x.GetType().Name);
				}
				catch
				{ }
			}
			finally
			{
				sw.Stop();

				if (total > 0)
				{
					e.Mobile.SendMessage(hue, "{0:#,0} doors have been imported in total.", total);
				}
				else
				{
					e.Mobile.SendMessage(hue, "No doors have been imported.");
				}

				e.Mobile.SendMessage(hue, "Door import completed in {0:F2} seconds.", sw.Elapsed.TotalSeconds);
			}
		}

		public static Dictionary<Map, int> ExportDoors()
		{
			return ExportDoors(DoorsPath);
		}

		public static Dictionary<Map, int> ExportDoors(string file)
		{
			var result = new Dictionary<Map, int>();

			var doors = new List<BaseDoor>();
			var regions = new List<Region>();

			var doc = new XmlDocument();

			var root = doc.CreateElement("doors");

			XmlElement map, reg, link, door;
			IPooledEnumerable<Item> o;
			BaseDoor bd;
			BaseHouse bh;

			int total = 0, count, subcount;

			foreach (var m in Map.Maps.Where(m => m != null && m != Map.Internal))
			{
				count = 0;

				map = doc.CreateElement("facet");

				map.SetAttribute("index", XmlConvert.ToString(m.MapIndex));
				map.SetAttribute("name", m.Name);

				regions.Clear();
				regions.AddRange(m.Regions.Values);

				if (m.DefaultRegion != null && !regions.Contains(m.DefaultRegion))
				{
					regions.Add(m.DefaultRegion);
				}

				foreach (var r in regions.OrderByDescending(r => r.ChildLevel))
				{
					subcount = 0;

					reg = doc.CreateElement("region");

					reg.SetAttribute("name", r.IsDefault ? m.Name : r.Name);

					foreach (var b in r.Area.Select(b => new Rectangle2D(b.Start, b.End)))
					{
						o = r.Map.GetItemsInBounds(b);

						foreach (var d in o.OfType<BaseDoor>())
						{
							bd = d;

							if (doors.Contains(bd))
							{
								continue;
							}

							bh = BaseHouse.FindHouseAt(bd.Location, bd.Map, 20);

							if (bh != null && bh.Doors.Contains(bd))
							{
								continue;
							}

							doors.Add(bd);

							++total;
							++count;
							++subcount;

							door = doc.CreateElement("door");

							SerializeDoor(door, bd);

							if (bd.Link == null || bd.Link == bd)
							{
								reg.AppendChild(door);
								continue;
							}

							link = doc.CreateElement("doors");

							link.AppendChild(door);

							var ld = bd.Link;

							while (ld != null && ld != bd)
							{
								if (!doors.Contains(ld))
								{
									doors.Add(ld);

									++total;
									++count;
									++subcount;

									door = doc.CreateElement("door");

									SerializeDoor(door, ld);

									link.AppendChild(door);
								}

								ld = ld.Link;
							}

							reg.AppendChild(link);
						}

						o.Free();
					}

					regions.Clear();

					if (subcount > 0)
					{
						reg.SetAttribute("count", XmlConvert.ToString(subcount));

						map.AppendChild(reg);
					}
				}

				if (count > 0)
				{
					map.SetAttribute("count", XmlConvert.ToString(count));

					root.AppendChild(map);
				}

				result[m] = count;
			}

			if (total > 0)
			{
				root.SetAttribute("count", XmlConvert.ToString(total));

				doc.AppendChild(root);

				doc.Save(file);
			}

			regions.Clear();
			regions.TrimExcess();

			doors.Clear();
			doors.TrimExcess();

			return result;
		}

		public static Dictionary<Map, int> ImportDoors()
		{
			return ImportDoors(DoorsPath);
		}

		public static Dictionary<Map, int> ImportDoors(string file)
		{
			var result = new Dictionary<Map, int>();

			var doc = new XmlDocument();

			doc.Load(file);

			var root = doc["doors"];

			if (root == null)
			{
				return result;
			}

			var doors = new List<BaseDoor>();
			var link = new List<BaseDoor>();

			int index, count;

			Map map;
			BaseDoor door;

			foreach (XmlElement m in root.GetElementsByTagName("facet"))
			{
				count = 0;
				map = null;

				index = XmlConvert.ToInt32(m.GetAttribute("index"));

				if (index > 0 && index < Map.Maps.Length)
				{
					map = Map.Maps[index];
				}

				if (map == null)
				{
					map = Map.Parse(m.GetAttribute("name"));
				}

				if (map == null)
				{
					continue;
				}

#if STRICT
				Region reg;

				foreach (XmlElement r in m.GetElementsByTagName("region"))
				{
					var name = r.GetAttribute("name");

					if (name == map.Name)
					{
						reg = map.DefaultRegion;
					}
					else if (!map.Regions.ContainsKey(name))
					{
						continue;
					}
					else
					{
						reg = map.Regions[name];
					}

					if (reg == null)
					{
						continue;
					}

					foreach (XmlElement o in r)
					{
#else
				foreach (var o in m.GetElementsByTagName("region").OfType<XmlElement>().SelectMany(r => r.OfType<XmlElement>()))
				{
#endif
					switch (o.Name)
					{
						case "door":
						{
							door = DeserializeDoor(o);

							if (door != null)
							{
								AddDoor(doors, door, map);

								++count;
							}
						}
							break;
						case "doors":
						{
							link.Clear();

							foreach (XmlElement d in o.GetElementsByTagName("door"))
							{
								door = DeserializeDoor(d);

								if (door != null)
								{
									AddDoor(doors, door, map);

									++count;

									link.Add(door);
								}
							}

							if (link.Count > 1)
							{
								for (var i = 0; i < link.Count; i++)
								{
									link[i].Link = link[(i + 1) % link.Count];
								}
							}

							link.Clear();
						}
							break;
					}
				}
#if STRICT
				}
#endif
				result[map] = count;
			}

			doors.Clear();
			doors.TrimExcess();

			return result;
		}

		private static void SerializeDoor(XmlElement node, BaseDoor door)
		{
			node.SetAttribute("type", door.GetType().Name);

			node.SetAttribute("x", XmlConvert.ToString(door.X));
			node.SetAttribute("y", XmlConvert.ToString(door.Y));
			node.SetAttribute("z", XmlConvert.ToString(door.Z));

			node.SetAttribute("ox", XmlConvert.ToString(door.Offset.X));
			node.SetAttribute("oy", XmlConvert.ToString(door.Offset.Y));
			node.SetAttribute("oz", XmlConvert.ToString(door.Offset.Z));

			node.SetAttribute("oid", XmlConvert.ToString(door.OpenedID));
			node.SetAttribute("cid", XmlConvert.ToString(door.ClosedID));

			node.SetAttribute("locked", XmlConvert.ToString(door.Locked));

			if (door.KeyValue > 0)
			{
				node.SetAttribute("keyval", XmlConvert.ToString(door.KeyValue));
			}

			if (door.OpenedSound > 0)
			{
				node.SetAttribute("osound", XmlConvert.ToString(door.OpenedSound));
			}

			if (door.ClosedSound > 0)
			{
				node.SetAttribute("csound", XmlConvert.ToString(door.ClosedSound));
			}

			if (door.Hue > 0)
			{
				node.SetAttribute("hue", XmlConvert.ToString(door.Hue));
			}
		}

		private static BaseDoor DeserializeDoor(XmlElement node)
		{
			var t = node.GetAttribute("type");

			var type = ScriptCompiler.FindTypeByName(t, true) ?? //
					   ScriptCompiler.FindTypeByFullName(t, true) ?? //
					   Type.GetType(t, false, true);

			BaseDoor door;

			try
			{
				door = (Activator.CreateInstance(type, _ImportArgs) ?? Activator.CreateInstance(type)) as BaseDoor;
			}
			catch
			{
				return null;
			}

			if (door == null)
			{
				return null;
			}

			var x = XmlConvert.ToInt32(node.GetAttribute("x"));
			var y = XmlConvert.ToInt32(node.GetAttribute("y"));
			var z = XmlConvert.ToInt32(node.GetAttribute("z"));

			door.Location = new Point3D(x, y, z);

			var ox = XmlConvert.ToInt32(node.GetAttribute("ox"));
			var oy = XmlConvert.ToInt32(node.GetAttribute("oy"));
			var oz = XmlConvert.ToInt32(node.GetAttribute("oz"));

			door.Offset = new Point3D(ox, oy, oz);

			door.OpenedID = XmlConvert.ToInt32(node.GetAttribute("oid"));
			door.ClosedID = XmlConvert.ToInt32(node.GetAttribute("cid"));

			door.Locked = XmlConvert.ToBoolean(node.GetAttribute("locked"));

			if (node.HasAttribute("keyval"))
			{
				door.KeyValue = XmlConvert.ToUInt32(node.GetAttribute("keyval"));
			}

			if (node.HasAttribute("osound"))
			{
				door.OpenedSound = XmlConvert.ToInt32(node.GetAttribute("osound"));
			}

			if (node.HasAttribute("csound"))
			{
				door.ClosedSound = XmlConvert.ToInt32(node.GetAttribute("csound"));
			}

			if (node.HasAttribute("hue"))
			{
				door.Hue = XmlConvert.ToInt32(node.GetAttribute("hue"));
			}

			if (door.Open)
			{
				door.ItemID = door.OpenedID;
			}
			else
			{
				door.ItemID = door.ClosedID;
			}

			return door;
		}

		private static void AddDoor(List<BaseDoor> doors, BaseDoor door, Map map)
		{
			var o = map.GetItemsInRange(door.Location, 0);

			foreach (var d in o.OfType<BaseDoor>().Where(d => d != door && !doors.Contains(d)).ToArray())
			{
				d.Delete();
			}

			o.Free();

			doors.Add(door);

			door.MoveToWorld(door.Location, map);
		}

		private static readonly object[] _ImportArgs = {((DoorFacing)0)};
	}
}