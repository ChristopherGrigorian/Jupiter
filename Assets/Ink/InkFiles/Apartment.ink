VAR ReachedGov = false
== LIV ==
You're in the living room of the apartment. Your kitchen is visible from here. You television set is still in the dimness of your surroundings.

+ _Turn on your television_
-> TEL
+ _Go to the kitchen._
-> KIT
+ _Go back to the balcony._
-> BAL
{ReachedGov == false:
+ _Leave the apartment._
-> HAL
}

== TEL ==
You try to turn on the television, you hear a soft whirring, so you know it has power, but it just won't turn on.
->LIV

== KIT ==
You go the kitchen and open the cupboards. You see some of your favorite snacks here--salt and vinegar chips, pretzels... You take them.
->LIV

== BAL ==
The balcony is quiet and empty. The street is still below. There is a lovely garden table set.
{ReachedGov == true:
+ _Kill yourself._
You once again throw yourself from the balcony into the empty night.
    -> AfterParty
}    
+ _Return to the living room._
    -> LIV
    
== HAL ==
You open the door to your apartment. You're scared a siren is going to go off, but nothing of the sort happens. You walk down the hallway. It's an endless sea of hallways. You follow exit signs and wind yourself all the way to the front of the building on the outside.
The air is still and without substance.
The whole world around you pulses with chromatic abberation.
The buildings continue to fold in and out of existence. You continue to roam around until the government building contructs itself within view.
You try for the door, the building is unlocked.
You walk through the halls, passing by rows of offices. You instinctually walk to to Almos' office.
His desk is covered in diagrams, and a map of the entirety of this realm. There are pictures plastered all over the walls of you, Boris and Rabbit. Each are lined with specific dimensions of their limbs and various features.
"What... the fuck?" you say aloud.
You feel Almos' presence overwhelming your own, "It's creepy, I know," he says. "It's how I was able to make a somewhat accurate proxy world."
"A duplicated world?" you ask.
"Yes, one where I had more control. In this one, we have none. This realm belongs to Bruce," he responds.

+ "Does he know we're here?"
- "Perhaps..." Almos drones. "There were no stars, moons, or planets in the sky, however... Perhaps he's losing his hold."

+ "Is Boris and Rabbit here?"
- "No... we're... probably the only one's jailed here..." he says. "I'm not sure how they escaped their own prisons, but they wound up back on Jupiter long before us."
You look puzzled, "So how were you able to string their appearences together for the proxy world?" you ask.
"Rabbit was trying to break us out from the outside. At work I was constantly receiving streams of data from a source I couldn't recognize. Rabbit was providing all the necessary information to manifest versions of Boris and themself to assist us."

You take a closer look at his desk.
"There's a locked drawer down there," Almos says. "Just shoot it, I don't remember where the key is..."
You manifest your gun and shoot the lock to open the drawer.
"Weapon data," Almos says. "Tons of it."
"But, this stuff isn't...manifestable... it's all on paper. Don't we need to be shown how to manifest something by someone else? Or see it manifested already?" you ask.
"That certainly true, but there's other methods. Rabbit will probably know what to do with it once we get out of here," he responds. 
You tuck the papers away.
Your stomach begins to turn--you're hungry.
You head to the breakroom. There's fresh chocolate muffins in there. They seem to be provided by someone named Marilyn because of a note left behind.
"So... we're 'probably' alone?" you ask.
"This is sort of why I always had doubts," Almos responds. "She's never revealed herself to me."

+ "Perhaps she's on the outside?"
"Maybe..." he says. "It could be a similar situation to Rabbit transmitting us stuff."
"Hopefully, she's an ally," you respond. "I hope these muffins aren't just meant for temptation to blindly like her."
"One can only hope," Almos says. "I think it's about time we head back to the apartment...don't you?"
"Yes... I suppose," you respond.
You walk all the way back to your apartment. You're back in the living room.
~ ReachedGov = true
-> LIV
+ "Maybe we should wander and look for her?"
"Not... a good idea..." Almos says cautiously.
"Why not? She could help us!" you say.
"We have no idea if this muffin thing is just a temptation trap for us to blindly like her," Almos explains. "She could be our opposition for all we know. Besides, she's never revealed herself to me... it's just way too risky."
"You're right... we don't need to look for trouble right now..." you say.
"I think it's about time we head back to the apartment... don't you?"
"Yes... I suppose," you respond.
You walk all the way back to your apartment. You're back in the living room.
~ ReachedGov = true
->LIV
->DONE