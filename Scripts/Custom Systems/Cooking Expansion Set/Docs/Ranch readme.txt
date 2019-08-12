BaseAnimal 2.0

BaseAnimal 2.0

 This is the Update of BaseAnimal. I've made a number of changes to the original script...such as requires a new thread.

 BaseAnimal is a package which allows for your animals to act more like animals. Deer run from players, wolves hunt deer. Bears steal your food and eat your crops. An so on.

 2.0 brings a new element to the system. Animal Husbandry. You want to be a farmer? Now you can. I've included a number of farm animals, so to give examples on how this system can be used. Most of them have specific breeds. Cows, for example, have breeds for milk and meat. Sheep have milk, meat and wool breeds. They can be cross-bred but the best milk-givers are purebreds. Chickens lay eggs daily in nests.

 Animals can also age now. They're born, they age, they reproduce and they die (unless they're killed before then).

 This system should be backward compatible. A simple command will turn all current BaseAnimals into 2.0 compliant critters (otherwise they are babies). [global set update true where baseanimal (I think that's correct).

 With this system, I've added a number of things. The various animals have different cuts of meat...which are all cookable. I've added a MilkingBucket, MilkKeg, ButterChurn, CreamPitcher, MeasuringCup. A number of these adds concern Vhaerun's Food system. I wasn't happy with how one made cream with the flour sifter from...not milk, but water. The measuring cup fixes this. The MilkKeg is where milk is collected after milking. Can be poured into pitchers from the keg, for drinking. Or the CreamPitcher can target the keg and skim cream. The Measuring cup may be targeted by either the keg or cream pitcher to get a cup of cream or milk. I made it to display the age of milk (loosely).

Wild animals are BaseAnimals that can be turned into mounts with the Bridle or pack horse/llama with the saddlebag. There are two distros here. Included just so when you release the pack animal, it shakes off its pack and becomes a wild animal again. (still need to work it out so it follows the rules of the WildHorses though).

Ranch. BIG add. Ranch Stone, Ranch Extension Deed, Fences, Gates, Fencing Hammer, Branding Iron and Fencing Kit. And of course a stockman (rancher) to sell ranch stuff.
 The ranch stone is the heart of the ranch. Only the owner (the one who placed it) can access the gump. With the gump you can get the Fencing Hammer and Branding Iron. You can also transfer ownership of any branded animals to another player, transfer the ranch to another player, unlock (movable true) all the fences/gates, absorb a nearby ranch that you already own or redeed the ranch stone. When the stone is redeeded it unlocks all the fences of that ranch (fences placed with that fencing hammer) while leaving any neighboring fences locked. The ranchstone can only be placed in legal housing regions.

 The branding iron works like this. When you first tame a baseanimal, you can brand it. You then become the owner of it and it displays your brand. When it produces offspring, if it's the mother, you retain ownership of those offspring, but your brand is gone. You may brand those offspring without first taming them. If you fail to brand them, and they produce offspring, you no longer own them and you must tame them to brand them.

 When you reclaim an animal, if you're the owner, you automatically get control of it...as if you tamed it. I went under the assumption that you needed to tame it to begin with. Transfer Animal works about like this except you give the animal to another player.

 The fencing hammer sets the fences and gates into the ground (so they don't decay). Both must be purchased from the Stockman or crafted with the Fencing Kit. Gates may only be opened by the owner of the ranch, to prevent his livestock from escaping.

 And finally, the Mildred the Animal Trader. She doesn't actually sell any animals. I, um, couldn't figure out how to get her to sell all the different types of breeds. But she will buy them. I think the prices may be a bit low (especially since some of them are resource producers) but you can easily tweak that in the script.

 If you have any issues I will support as best I can. If you have any comments or suggestions, I would love to hear them. Feel free to change whatever you want. The system still doesn't work exactly how I want it, but that's the limit of my abilities, for the moment. 