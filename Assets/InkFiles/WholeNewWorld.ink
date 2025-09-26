== WNW ==
You awaken in a cylindrical chamber. On the other side of your glass prison is a room filled with wires and monitors. 
You feel panicked. Your memories feel incomplete and fragmented. You know not whether you were being experimented on.
This place feels wholly unfamiliar to you.
You hold your palms to your face.
You feel yourself to be a mixture of both man and machine. Equivocally, they blur into the same thing. 
You metallic head falls into your metallic hands in frustration. They grind and clink upon each other.
You feel everything, as if the very metal itself was imbued with nerves.
You touch the glass that encapsulates you. 
You feel the friction against your fingertips. 
You begin to panic and become slightly claustrophobic. 
The room itself appears to be holding cell. There's a small window on the door into the cell. You see a hallway through the little square of glass.

+ _Break the glass._
    -> GlassBreak

+ _Watch and observe the hallway._
    -> ObserveHallway


== ObserveHallway ==
You try to calm yourself down before making any rash decisions. As a machine, you know you can't breathe, but you feel as though you could. A breath swells in your throat and stays there with no release or entry. 
You close your eyes, and swallow hard. You're going to break through the glass.

+ _Break the glass._
    -> GlassBreak
    
==  GlassBreak ==
You ball your fist, each finger curls without effort into place.
Your fist bursts through the glass. The whole front of the capsule collapses.
A wave of vertigo washes over you. A siren blares. You know not who it alerts, only that you're in deep trouble.

+ _Examine the technology within the room._
    -> ExamineTech
+ _Try the door and escape the cell._
    -> BreakOut
    
== ExamineTech ==
The wires and monitors within the room are completely foreign to you. You know not of their purpose, only that they were most likely either watching or ensaring you. On closer examination, each device is engraved with "Property of LOTUS."

The siren continues to blaze on. You waste no time in challenging the cell door.

+ _Try the door and escape the cell._
    -> BreakOut

== BreakOut ==
The door jams and refuses to open. 
You see no purpose in restraining yourself any further. You hold out your palm and a revolver materializes in your hand as it once had in the proxy world.
You shoot the lock and break the door down with a strong kick.

You exit the room and are immediately met with a diverging path. Jail cells just likes your line both walls to point of the space looking liminal. You can hear a faint shuffling and buzzing noise echoing from the hall on your right.

+ _Head left_
    -> LeftHall
+ _Head right_
    -> RightHall

== LeftHall ==
You walk cautiously down to the left. Perhaps it's too late to exercise caution considering the alarm you set off, but you hope to mask your exact location as much as possible. 
Just as you're about to take a turn you see figure approaching you at the intersection.
You approach without hesitation, manifesting your weapon once more.
"Woah, woah, woah!" says the figure. "Hellooo there!"

+ "Boris?"
    ->LeftHallContinued

== RightHall ==
You bolt to the right. Whatever may test you, you're willing to face the threat head on.
You rush to the noise. A robot with two little wheels for movement turns a corner and comes swirling down the corridor towards you.
You feel Almos' combat prowess besides your own, making you proficient with both swords and guns. Click on the 'Inventory' button, and decide which weapon you will focus on manifesting for this battle.
You manifest your weapon. Prepare for combat.

~ StartCombat("RightHall", "RightHallFightFinish")
->DONE

== RightHallFightFinish ==
// Give item here
The robot crumbles to your strength. You wander the halls where the robot came from only to be met with dead ends.
You trace your steps back to your original cell, only to see another figure approaching you.
You approach without hesitation, manifesting your weapon once more.
"Woah, woah, woah!" says the figure. "Hellooo there!"

+ "Boris?"
    ->LeftHallContinued
    
== LeftHallContinued ==
"Whaddya think." he responds.
"How are you here?" you ask.
Boris lowers his voice a little, "There's a couple of robots back there, and I got scared."
"No, not like... whatever," you concede. "Do you know the way out of here?"
"Yeah," he responds. Boris doesn't continue speaking.
"...so..." you say after an uncomfortable amount of time.
"What?" says Boris.

+ "Have you no urgency?"
    - "Why are you in a hurry?" he says. 
    "You must be joking, just get us out of here," you say.
    "Alrighty! Like I said though, we have to probably kill some robots in the way first."
    ->LHC2

+ "GET US OUT THEN."
    - "Damn. Alright." he mutters. 
    {MuffinShared == false: 
    "First you don't share the muffin, and now you're just shouting at me. You really are just a bitch, huh."
    Your mouth drops, you've nothing left to say. "Close your damn mouth, let's go," Boris says.
    You follow without a word.
    }
    ->LHC2
    
== LHC2 ==
You both wind through endless halls until finally arriving before the robots Boris was talking about.
"Will you be joining me for this fight?" you ask.
{MuffinShared == false:
"I don't know, man. When I needed help, you didn't provide."
"You wanted a fucking muffin dude, get over it."
"Alright my bad...fatass," he mutters under his breath.
- else:
"Of course! My bloods boiling!" he says.
"Well... it would if I had any," he mutters. "Let's get em!"
}

~ AddCharacter("Boris")
Boris has now joined your party.
Prepare for combat.
~ PlaySong("Bees")
~ StartCombat("LeftHall", "JailFinish")
->DONE

== JailFinish ==
~ StopSong()
"Phew!" exhales Boris. "Hopefully no more of those freaks are around."
You look at yourself once more, raising your hands in disbelief.
"I'm a robot too," you say.
"Yes, you're a freak too," says Boris.
"How are you still... human?" you ask.
"It's cause I'm not real!" Boris says.
"Really?" you respond.
"No, haha!" Boris smiles. "I went shopping for a face. You've just been stripped of everything that makes you... well... you."
"So, I don't have a face right now?" you ask.
"Yep! Well, not in the traditional sense" says Boris. "It's smooth and metallic, though. You of course have access to all the senses, hence you being able to see...," he pauses, "MY... GORGEOUS... MUG..."
Boris strikes a pose and flexes.
You roll your eyes, "Vanity will be your end, you know?"
"I think you speak a little too close to home," Boris says. "I'm but a product, nothing of this vessel is my own creation."
"You're very cryptic, honestly," you say.
Boris stares at you intently, "Honestly, I'm suprised you don't remember anything, they really did wipe everything from you."
"See, there you go again!" you exclaim.
"No matter," Boris says. "Let's get out of here."
Boris leads you through the facility. You eventually break out to the outside world. You feel a harsh wind. The ground you walk upon feels flimsy. A desolate vision, if anything.
You look to Boris, "Not many guards around here for a facility like this," you say.
"You were never supposed to escape," he responds. "Anyway, now that we're out of the building our signal can be received." 
"Have you ever teleported before?" he says. "Actually, don't answer, you probably haven't, just grab my hand, I know a safer spot."
You grab Boris' hand. 
"Concentrate with me, I'm going to transfer you some location data," he says.
You close your eyes and your mind is overwhelmed by a beautiful, small town. You see condo like, colorful homes seemingly on the outskirts of a grand chateau.
"Okay, you may open your eyes now," says Boris. You open your eyes. 
"You should have the proper data to teleport now. Open up your map, and select Galagos in the Galaga region. You'll no longer have to focus on a region to warp there. The system will help facilitate the experience and transport you."
VAR FirstTimeGalaga = true
~RevealMapButton()
~UnlockLocation("Galaga")
~UnlockSubLocation("Galagos")

->DONE
