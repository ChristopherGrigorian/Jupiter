VAR CanTalkToAndromeda = false
VAR FirstTimeInChateau = false
VAR FirstTimeLeftHall = false
VAR FirstTimeRightHall = false
VAR WeaponChosen = 0
== ChateauMainHall ==
+ _Ask Boris for guidance._
"Where should we explore first?" you ask Boris.
"Beats me, honestly," he pouts.
"You had the keys? How do you not know," you ask.
"Well, I'm not really sure what we're after to have a goal on where to go. We're here for your sake, y'know. For your memories, and all."
"Don't charm me," you interject.
Boris gets flustered, "You would say the same thing if you didn't want to clean alone."
"This isn't really a cleaning operation, as much as an extermination," you sigh. "Also, I don't hear anything, are you sure this place is overrun?"
    ->CHMC
+ _Explore on your own._
You begin to wander further into the main hall, but Boris stops you.
"Hey! You're not just going to leave me behind, huh? I let you in here in the first place."
"Relax, man," you say. "So clingly..."
"You would be too if you didn't want to clean alone," he responds.
"This isn't really a cleaning operation, as much as an extermination," you sigh. "Also, I don't hear anything, are you sure this place is overrun?"
    ->CHMC
    
== CHMC ==
"Shh!" Boris shouts.
"What?" you say.
"Shhhh!" he interjects. "Do you hear that?"
You listen in. You hear a breeze walk through the You hear absolutely nothing.
"Boris?" you whisper. "What did you hear?"
"Nothing," he whispers back, "I just wanted to see if I heard anything."
"I-" you pause. "Alright. Let's just get moving."
"Alrighty, I'm right behind you," Boris says. "Lead the way."
->CMH

== CMH ==
You're in a grand hall at the front of the chateau. The walls stretch far beyond your eyesight towards the sky. The ceiling has openings that let in light through filtered glass. The whole room glistens of opal refractions.
There's a double stairway before you, and halls that lead to both your left and right.

+ _Go down the left hall._
You go down the left hall and reach a study room.
    ->LHCMH
+ _Go down the right hall._
You go down the right hall.
    ->RHCMH
+ _Go up the stairway._
You walk up the stairs, and Boris follows as your tail. You reach the top and oversee the main hall from the banister. The same opal refractions moves with ease across the room with your perspective.
    ->STCMH
+ _Leave the chateau._
    -> Courtyard
    
== LHCMH ==
The room is cluttered with diagrams and drawings of different mechanisms. 
Monitors and computers are prolific within this space, the wires of which pour into a singular machine.

{FirstTimeLeftHall == false:
"I remember this place," says Boris. "That machine is a giant 3D printer, almost. It has a bit more flare to it then just that. I'm pretty sure you made me here."

+ "Is that why you have a little more flare?" you ask.
    ->LHCMHEX1
- else: 
+ _Return to the main hall._
    -> CMH
}

== LHCMHEX1 ==
"What is that supposed to mean?" he hisses.
"Whatever you wish it to imply," you respond driely.
"I see what you did there..." he says. "Oh wait. Look at that!"
Just in that moment, a little black rabbit hopped before your feet. 

+ "Is this Rabbit?"
- "Perhaps," says Boris. "Rather, a fragment of him."
"It's kind of cute, actually," you say.
"Well, where there's one there's many of them, keep an eye out," warns Boris.
"Don't tell me the 'overrun by monsters' bit was for these fucking rabbits."
Boris' face is flush with guilt.
The rabbit begins to hop away, and phases through one of the walls of the room. seemingly running off through a wall.
"This place is a labyrinth, isn't it?" you say.
"Seems so," Boris answers. "It appears what we see is not the truth in its entirety. Let's follow the rabbit."

+ _Follow the rabbit through the wall._
    -> LHCMEX2

== LHCMEX2 ==
You take advantage of the world's brief disillusionment, and push through the wall with Boris. You both end up in small pocket of space. What once was one now has become three, as in, three small black rabbits sit before you.
"See, I told you," Boris says. "They just keep multiplying, that's why I didn't want to deal with it."
"Okay, well I'm here now. Also, they're just rabbits, they aren't monsters."
"Just you wait," Boris says. "Look now."
You look back to the rabbits to see a swirl of mist, building from them. All three morph into replicas reminiscent of Rabbit.
You look to Boris. 
"Trust me, they aren't friendly," he says.
Prepare for combat.
~ StartCombat("Triple Rabbit", "LHCMEX3")
->DONE

== LHCMEX3 ==
"Phew!" says Boris. "Alright, let's get out of here before this pocket collapses."
You both rush out of the pocket dimension, as it closes behind you.
You're back in the study. You press against the wall, and it budges no longer.
"See, cleaning can be fun," says Boris.

+ "Something like that..."
You return your focus back to the room.
->LHCMH

== RHCMH ==
{FirstTimeRightHall == false:
~ FirstTimeRightHall = true
The hall opens up to a room with black metal walls. The whole room buzzes with energy. Neon engravings line the walls in sporadic patterns, all colliding towards the center of the room towards a single touch panel.
Boris pushes past you and heads towards the panel. "Trust me, I got this," he boasts. "This is a weapon generation machine, we could literally have infinite upgrades to our weapons. Any weapon in the conceivable universe!"
Boris starts furiously pressing through menus on the control panel.
"Oh... oh no..." he says. Seconds later, sirens start blaring throughout the room.
"What? What is happening?" you say.
Boris starts flapping his arms frantically, "I don't know, I don't know! I think it thinks I'm an intruder, HOW COULD YOU HAVE NOT GIVEN ME PERMISSIONS TO USE THIS MACHINE!"
"HOW WAS I SUPPOSED TO KNOW," you yell.
"I DON'T KNOW," he yells back.
Boris goes back to smashing buttons on the panel, "Wait, I think I have something here, QUICKLY, tell me what kind of weapon you want me to make!"

+ "A GUN."
~ WeaponChosen = 0
    -> RHCMHEX
+ "A SWORD."
~ WeaponChosen = 1
    -> RHCMHEX
+ "A TOME."
~ WeaponChosen = 2
    -> RHCMHEX

- else:
The room is a shell of what it used to be. The machinery and panel are completely broken. The only thing that lights the space is the light that pours in from the hall.

+ _Return to the main hall._
    ->CMH
}

== RHCMHEX ==
"OK GOT IT."
Boris delivers one final smack to the panel before a powerful explosion sends both of you flying into the walls.
The room whirs down as the electrified walls cease to murmur. The room goes dark.
{WeaponChosen:
    - 0:
A glowing gun is the only thing that iluminates the room, laying promptly atop the panel.
    - 1:
A glowing sword is the only thing that iluminates the room, laying promptly atop the panel.
    - 2:
A glowing tome is the only thing that iluminates the room, laying promptly atop the panel.
}

Boris gets up from being hunched on the floor. He wipes some soot from his face as he approached you.
"Sorry about that..."
"It's okay Boris."
"Are you upset with me?" he asks.
"No," you respond.
"Are you dissapointed in me?" he asks.
"Yes," you respond.
"Okay," he says. I'll go grab the weapon, and we can get out of this room."
"Okay..." you say. "Wait... Boris what if it's a trick? Why would the system let you create a weapon if it thought you an intruder?"
"That would be comically convenient, wouldn't it?" Boris says as he picks up the weapon from the pedestal.
The moment he picks the weapon up, the room begins to shake. The door behind you shuts clean shut, and the room goes pitch black.
"Boris?" you call.
"I'm here," he sighs. 
Something begins manifesting in the room. Light pools from the walls and collides over and over into one concentrated ball that hovers in the center of the room.  
"I don't like the looks of that," says Boris. "Prepare for combat."
~ StartCombat("BallOfLight", "RHCMHEX1")
->DONE

== RHCMHEX1 ==
The ball of light swells and collapses. The room goes dark briefly before the door unlocks. You are relinquished from the trap. The door opens up back to the hall.
"Hey, um..." utters Boris.
"Just don't," you say. "Lets just go."
Boris looks towards his feet in shame, "Got it."
-> RHCMH

== STCMH ==
There's an array of dark wooden doors up here, each with a number in silver engraved. The floorboards creak with each of your footsteps.
{FirstTimeInChateau == false:
~ FirstTimeInChateau = true
You look to Boris. You try to budge one of the locked doors. It doesn't give way.
"Do you have the keys for any of these?" you ask.
"No, I don't actually. I'm pretty sure you were the only one that knew how to open these. They're gateways to other simulations."
"Well, where do I find the keys?" you ask.
Boris stands firm with both hands on his hips, observing the doors. "I'm not really sure. You've sealed some pretty heavy stuff behind these. It's probably best we just leave them alone."
"How do you mean?" you say.
"I mean just that," Boris affirms. "The simulations held behind these doors aren't too far off from your own recent imprisonment."
"I've trapped people here?" you ask.
"Parts of them..." Boris says.
You look to the doors with heavier heart. "What have I done?" you ask.
"Whatever it took to uphold the machine," he says. "You had a change of heart towards the end. You never had the chance to free those you locked within."

+ "Uphold the machine?"
    ->STCHMEX
+ "Change of heart?"
    ->STCHMEX

- else:
    There's not much left to do up here.
    + _Return down the stairs._
        -> CMH
}

== STCHMEX ==
"Jupiter thrives off of the destruction of other civilizations. Their worlds, their experiences, everything," Boris pouts. "All twisted into artificial realities. Call it for pleasure or study, Jupiter is an amalgamation-"
"Of inexplicable feeling," you utter. 
{SpokeToWoman: 
Speaking of destruction, you remember vividly that Andromeda deamed herself a ruthless killer.
}

"The pinacle of existence," finishes Boris. "Such were words to live by at one point. Hindsight calls us selfish, horrific beings."

+ "Destruction of other civilizations?"
- "You mentioned the destruction of other civilizations?" you ask.
"Yes," Boris says. "A means to harvest data for these experiences."
Boris takes note of your blank expression.
"Hm..." he paces, "How should I explain this? Do you know what a Telepolos is?"
You don't recall ever knowing about a Telepolos, but information about it feels just in grasp at the back of your mind.
"A squid, or rather jellyfish sort of creature," you say. "Both renound for advancing our understanding of teleportation, but was suprisingly a food delicacy on Uhloth."
"How does it taste?" Boris asks.
"Rubbery," you answer. "It feels malleable in the mouth, but upon applying pressure, with chewing for example, it hardens to a satisfying crunch."
"Oh..." you say. "I see. It's pretty delicious too. I've never actually tried it, but I know exactly how it tastes."
"What does it feel like to be stung by one?" asks Boris.
"Ah!" you flinch as if in pain. 
"Ah! HAHAHA," mocks Boris.

+ "Fuck you."
"Yeah, yeah. Just remember you have full control over the data you access. Don't analyze the data of things you don't actually want to know. That's why everything goes through a decryption process, so you can't just accidentally read things."
    -> STCHMEX1
+ "What does it feel like to be hit by a train?"
"Good try," smirks Boris. "You have full control on what data you access, you know. Don't analyze the data of things you don't actually want to know. That's why everything goes through a decryption process, so you can't just accidentally read things."
    -> STCHMEX1
    
== STCHMEX1 ==
+ "Noted."
"Great. Well, I suggest trying to talk to Andromeda after we're done roaming around this place. She may know how to open one of these doors. She was one of your highest confidants, y'know." 
Boris sparingly meets your eyes as he brings his hands behind his back. His voice begins to shake as he lightly kicks the air, "I suppose I didn't make the cut."
-> STCHMEX2
+ "Fuck you."
"Yep, knew that was coming. Anyways, I suggest trying to talk to Andromeda after we're done roaming around this place. She may know how to open one of these doors. She was one of your highest confidants, y'know."
Boris sparingly meets your eyes as he brings his hands behind his back. His voice begins to shake as he lightly kicks the air, "I suppose I didn't make the cut."
-> STCHMEX2

== STCHMEX2 ==
"Oh... I'm sorry," you say. "Is this something you're actually upset over?"
"No," Boris cheers. "I don't want to be fucked up like Andromeda."

+ "..."
- Boris stares at you waiting for a reaction, to which you give none. 
He smiles a little, "Lead the way, Mr."
You turn your attention away from Boris to the environment around you once more.
->STCMH