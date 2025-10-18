VAR FirstTimeOasis = true
VAR Drowning = true
VAR Seagul = 0
== Oasis ==
{FirstTimeOasis:
You're being is invited to a new space, and way of thinking. Your party and yourself materialize on a sandy beach. Seagulls chirp in the air. Waves thrash and roar and relief their anger to a rolling whisper.
->MainBeach
-else:
This is Oasis.
->MainBeach
}

== MainBeach ==
The sun beams down upon you. There's an unsurpassable cliff behind you that stretches with the coastline. The sandy coast, upon which you stand spreads in either direction as far as your eyes  can see. The whole place feels barren, and apart from the seagulls, umbearably quiet.

{FirstTimeOasis:
Andromeda walks up beside you, a light breeze brushes her hair. "We're here... I guess..."
+ "Do you feel anything?"
 -> AnDi1
+ "Do you know anyting about this place?"
 -> AnDi1
}

== AnDi1 ==
"No..." he says. "It's all just emptiness, I don't understand. Where's my other half?"
"I suppose we could walk along the coastline," says Boris. We kinda just have to pick a direction though and stick with it."
+ "Is there no better way?"
- Andromeda paces a bit, "I suppose not, right?
Boris looks to you, "Alrighty let's flip a coin, winner will decide what direction we'll go. Heads or tails?"
+ "Heads."
->Heads
+ "Tails"
->Tails
+ "You pick Boris, I don't mind."
"You don't have much of a head on your shoulders, do you?"
You stare blankly at Boris.
"No matter, let's just head to the left."
->BeachCycle


== Heads ==
Boris takes out a coin and flips it. "Heads!" he says, "I win!"

+ "I called heads?"
"You didn't call heads, you cheater, I did," you say.
"Why are you lying?"
You look at Boris as if you might blow a fuse. He seems unphased by bottled up anger. Andromeda can be heard giggling in the background.
"Alright we're heading right, let's go," announces Boris.
->BeachCycle
+ "Yay..."
You know Boris cheated but just decide to let it go. You take a deep breathe and exhale.
"Alright we're heading right, let's go," announces Boris.
->BeachCycle

== Tails ==
Boris takes out a coin and flips it. "Tails!" he says, "I win!"

+ "I called tails?"
"You didn't call heads, you cheater, I did," you say.
"Why are you lying?"
You look at Boris as if you might blow a fuse. He seems unphased by bottled up anger. Andromeda can be heard giggling in the background.
"Alright we're heading left, let's go," announces Boris.
->BeachCycle
+ "Yay..."
You know Boris cheated but just decide to let it go. You take a deep breathe and exhale.
"Alright we're heading left, let's go," announces Boris.
->BeachCycle
== BeachCycle ==
{Seagul > 10:
Your walk with the group is interrupted a malicious swarm of seaguls!
"Ah! I knew those bitches were evil," screams Boris.
"Calm down," Andromeda says as she manifests her weapon. "Get ready for battle."
~ StartCombat("Seagul", "SeagulAfter")
- else:
The group's footsteps imprint into the sand over and over again. It feels as though the beach is infinite in expanse. No landmarks could dictate from where your journey started nor how much progress you've made.
}

~ Seagul += 1

{Seagul > 3 && Drowning:
~ Drowning = false
"I feel like we're getting nowhere," Boris sighs.
"Maybe we're going about this all wrong. Perhaps the sea is the main attraction in this simulation?" says Andromeda.
"YOU WANT ME TO SWIM?" cries Boris. "No way."
"A bit more than that. I think we need to dive into the ocean," says Andromeda.
"Won't it feel like we're drowning?" you ask.
"Not if we never decrypt the associated data... and also encrypt the data current circulating within us about drowning."
+ "Let's just keep walking."
Andromeda looks to Boris and then back to you, "Ok well if you change your mind, we'll be ready to take the plunge."
-> BeachCycle
+ "Alright, let's dive in."
-> OceanMain
}

{Drowning == false:
+ _Keep walking._
->BeachCycle
+ _Dive in the water._
->OceanMain
- else:
+ _Keep walking._
->BeachCycle
}

== SeagulAfter ==
"That was rough," sighs Boris.  "What's the verdict mister? Where are we going?"
+ _Keep walking._
->BeachCycle
+ _Dive in the water._
->OceanMain

== OceanMain ==
{FirstTimeOasis:
The party wades further and further into the water. You all walk into the ocean until you're about chest deep.
Boris begins shaking, "Are we sure about this?"
"Absolutely," interjects Andromeda. "It's ok if you're scared Boris."
Andromeda waddles over to Boris while the water slowly swells to her neck and sinks again. She grabs Boris' hand beneath the water to comfort him. 
"We go," she says while looking towards you.
}

You all plunge through the surface of the water and dive deeper below. A limitless blue enshrouds you and the party.

{FirstTimeOasis:
"A damn! It's the same thing as the beach. Everything just feel infinite around here. Where the fuck is Lucy?" says Boris.
"How did you speak to us just now?" you ask.
"We don't communicate with sound, you know. Our mouths may move but words are transported with intent," informs Andromeda. "You may message anybody you know with just your thoughts, or if you'd like to voice to those in a vicinity, your message is broadcasted a distance from yourself."
Boris looks to you now, "Well you were the only connection with Lucy weren't you?"
You look to Boris, but don't respond. You don't really remember Lucy.
"MESSAGE HER," he yells.

+ "Ok, damn."
"LUUUCCCYYYY," you shout with all your might.
Boris begins to yell back "NOOO NOT SHOUT THE MESSAGE." He looks to you now all wild eyed, "YOU WERE SUPPOSED TO JUST SET UP A PRIVATE MENTAL CONNECTION, YOU JUST BROADCASTED THE MESSAGE TO EVERY-" 
The ocean begins to violently thrash and shake. A wailing noise vibrates through the water with such ferocity the party goes still in fear. 
"OH GOD FUCK" screams Boris.
A massive shadow of a figure encroaches upon the party.
Boris starts flailing about, trying to kick up to the surface as fast as possible.
Andromeda swoops over and grabs him by the foot holding him back, "HEY! You're not leaving us, we got this. Everyone, prepare for combat!"
~ StartCombat("Whale", "WhaleFinish")
->DONE
}

+ _Head to the underwater train station._
    -> UnderwaterTrainStation

== WhaleFinish ==
Boris can't stop hyperventalating. Both Andromeda and you look to Boris. "Huuumph... Hummmph," Boris' heavy breathing continues.
"Boris, dude, breathe," says Andromeda. "Just re-encrypt the file on thalassophobia."
"But I AM scared of lassos," Boris cries, "It's INTEGRAL to my personality."
"Oh my god," sighs Andromeda. 
"All those poor animals..." whispers Boris, "They stood no chance against cowboys."
You roll your eyes, but briefly catch a glistening coming from deeper in the water.

+ "Follow me."
- "Wait... what... where are you going?" Boris says while swimming behind you.
The party follows you in your pursuit of the glistening in the water.
-> UnderwaterTrainStation
+ "Do yall see that?"
Andromeda traces her eyes in the direction you're looking. She squints a bit, "Yes, actually. It's definetly worth investigating.
The party follows you in your pursuit of the glistening in the water.
-> UnderwaterTrainStation

== UnderwaterTrainStation ==
{FirstTimeOasis:
The party swims closer to the depths of the oceans. A building that resembles a train station begins to swell into sight.
"Hey guys, I think that's a train station," you say.
Boris' eyes widen, "Train station? How do you know it's a train station? It could just be any sort of building. Did a mysterious narrator from another world inform you that it was a train station?" 

+ "No, you psycho, it just looks like one," you say.
Boris looks to you in disapproval. 
Andromeda tilts her head slightly in confusion. "Yeah it doesn't really look like a train station dude," she snickers.
"THERE'S TRAIN TRACKS STICKING OUT ON EITHER SIDE OF IT," you yell.
Both Andromeda and Boris explode into a fit of laughter.
    -> UTSpecial
+ "Yes, actually."
"Oh, you've really gone mad now, haven't you?" Boris sighs. "All this medication in our database and we still don't have a cure for what you have..."
"Boris you're all fucked up too, I don't want to hear it," Andromeda interjects. 
    -> UTSpecial
}

In this limitless blue, you stumble upon train station. The building is covered in mosaic like circle windows, through each you can see a multicolored shimmer seeping into the surrounding water. Train tracks sprout from either side of the building.

+ _Enter the train station._
-> TSHub

== UTSpecial ==
You grit your teeth in frustation, but let the whole thing go. You attention is drawn back to the building. It is covered in mosaic like circle windows. They are gorgeous and grand. Through each you can see a multicolored shimmer seeping into the surrounding water, most probably due to a light coming from the inside. Train tracks sprout from either side of the building.

You swim towards the building and the party follows. 
You approach grand double doors at the front of the building. You reach the handle, only for Boris to shout at you for doing so.
"WON'T THE WATER GO IN? WAIT!"
You look to him all droopy eyed.
+ "There's only one way to find out."
- You reach for the door and throw it open. No water falls forward. The party walks through the entrance.
You all look behind you upon entering, only to see a wall of water standing tall by the door frame. 
"It... can't get in here?" utters Andromeda.
"I guess not..." Boris says while grasping at his clothes. "We're also... dry... What tech made that possible?"
"Not that it matters right now," says Andromeda. "Can we just focus on finding Lucy?"

+ "Sure."
-> TSHub
+ "Andromeda is right Boris, let's keep moving."
-> TSHub


== TSHub ==
The the perimeter walls of the train station are lined with soft glowing lamps. A gentle light pours from each of them. The walls seem to be made of polished concrete. A small tinge of a pinkish-red is seen in the material.
The party's steps coalesce and echo through the chamber. 
{FirstTimeOasis:
"What is this?" says Boris.
"Your guess is as good as mine," Andromeda says. "Does it feel like we're being watched?"
"Perhaps..." says Boris. He begins itching himself near the collar, "I'm getting kind of freaked out, actually."
You begin examining the space more thoroughly.
The room is grand in scale, and very barren. A few benches are sprawled about, and are made of the same material as the walls. If one squints, they would appear to be lumps that just swell from the floor. The room is split down the middle by the train tracks. The party stands together on the platform. A blinking sign can be seen hanging from the ceiling. A train is about to arrive. 
You almost missed it when you walked in, but there is an information kiosk a little behind you as a compartment attached the wall.

+ _Approach the kiosk._
    -> Kiosk
+ _Wait for the train._
    -> TrainWait
}

The room is grand in scale, and very barren. A few benches are sprawled about, and are made of the same material as the walls. If one squints, they would appear to be lumps that just swell from the floor. The room is split down the middle by the train tracks. The party stands together on the platform. A blinking sign can be seen hanging from the ceiling. A train is about to arrive. 

+ _Wait for the train._
    -> TrainWait
    
== Kiosk ==
The party approaches the kiosk.
{FirstTimeOasis:
"Oh, how lovely, a brochure," Boris picks it up and starts onfolding it.
"Oh wow," he turns the sprawled out brochure every which way, even upside down. 
"Yes... I see... hm... very interesting...yes... very nice..."

+ "What does it say bud?"
    ->Kiosk1
}

== Kiosk1 ==
Boris looks to you, "I don't know bud."
"I thought as much," you say.
You reach to grab the brochure out of Boris' hand but he pulls it away.
"Hey! Get your own man," he says.

+ "You're not reading it any way..."
- "The pictures are nice," he pouts.
"Fine..." you say.
You grab a brochure off the kiosk. You open it up and see that it actually doubles as a metro system, not of actual stops, but of writings similar to the note you found behind the door.
"This is location data, isn't it?" you ask while showing the page to Boris.
He peers over his own brochure and onto yours, "Yeah!" he says. "Would you like me to read it and send over the location data?"
"Sure, that would be great" you say. 
Boris begins analyzing the location data, and you see a tinge of confusion hit his eyes. 
Andromeda notices too. "Boris..?" she says.
+ "Is something the matter?"
- "No not necessarily," he mutters. "These zones just have strange names to them... they also come with danger warnings. I don't think we're cut out for any of these zones yet at our combat prowess, but I'll give them to you anyway. You can access them from your map feature while at a train station like this one."
"For now though..." he continues, "let's just go wait for the train."
"Thanks, Boris," you say.
"No problem, friend..." Boris coos.
The party heads back closer to the tracks to wait for the next train.
-> TrainWait

== TrainWait ==
{FirstTimeOasis:
"Where are we heading?" asks Andromeda.
"Whever this train takes us," says Boris.
"Great plan..." mutters Andromeda.
"Well do you have a better plan?" snaps Boris.
"No," says Andromeda.
"WELL GOOD THING I'M THE BRAINS OF THIS WHOLE OPERATION."

+ "I wouldn't say that..."
Boris' eyes laser over to you.
"Just you watch," he says, "This is exacly how we're going to find Lucy."
"Alright..." you concede.
A gush of begins to fill the chamber well before the train fullfills the space. You watch as carriages rush by and slowly screech to a halt before you. The doors open, inviting you within. The party enters.

"Wow... this is fancy..." says Boris.
An unfamiliar voice echoes through the cabin "I'm glad you think so..."
The party whips their head towards the voice.
A young women is sat in one of the chairs. She looks upon you all with eyes filled with sorrow and complacency. 
Boris begins smiling, "Hi miss..." he's incredibly giddy with joy.
"What's your name?"
"Lucy," she responds.
"YES! YES! FUCK YEAH I TOLD YOU BOTH," Boris shouts as he punches the air in triumph.
The woman's expression does not change as she looks from Boris to both you and Andromeda.
"I take it I'm who you're looking for," she says.
    -> TrainWait1
}

The train has arrived, please select a location from your map feature you wish it to take you.

== TrainWait1 ==
+ "Yes, actually."
"May I ask your names?" Lucy says.
"Sure, I'm Aiolos... probably... and this is Andromeda."
Lucy looks upon you both with contempt, "Andromeda..." she whispers. "I finally get to meet you, after all the anguish you've caused me."
-> LucyLament

+ "We need to murder Andromeda and observe you."
Lucy seems unphased by the notion, "Andromeda..." she whispers. "I finally get to meet you, after all the anguish you've caused me."
-> LucyLament

== LucyLament ==
Andromeda appears to be frozen in both fear and distress.
Lucy inhales deeply, "I've only saw a glimpse of what we've done..." she begins to shake. "This whole world is just a manifestation of my grief," she says. "Bruce abandoned me... and he still abandoned you..." she grits her teeth. "YOU SELFISH PIECE OF SHIT!"
Boris opens his mouth slightly as if he were wanting to interrupt, but he continues to remain silent.
"You weren't useful to him anymore, once you got a fucking heart from me," she cries. "And while you got to live a fairy tale in his simulation, I was left here to just rot away and deal with what you've done."
"You had ever possibility to make things right," Lucy shouts, "You waited for one of Bruce's clones to show up before actually getting up and doing something?" 
"Bruce didn't abandon me!" Andromeda cries. "Aiolos is living proof of that."
"Oh spare me," Lucy says. "You have no clue what Aiolos desires."
"I'm..." Andromeda says. She stumbles on her words, "I'm going to stop it. Right here right now, you won't have to live with that grief once I'm gone."
"You don't know that," Lucy says. "You have no proof that one of our anchors will win over the other."
"You've thought of the same solution then..." Boris interrupts.
"Yes, I've thought of how stupid that sounds," she snarls, "And another thing, there you go again being selfish. What makes you think that you deserve to die over me?"
"I don't... I just wanted to be able to give you your spark back..." Andromeda whispers.
"We both know we lost it long ago," she says. "The promise of happiness becomes so lackluster once you realize its cost."
Lucy calms down a bit, "I shouldn't be made at you. I'm sorry. We once were one. You're just the part of me I wish I could forget."

"Aiolos..." Lucy says. "I know not your desires or what you wish to cultivate for this universe. Be it chaos or dignity, I want you to kill me rather than Andromeda. I will be an experiment no longer, or a means of upholding your machine." 
"I was bound here as a means of upholding Andromeda's combat prowess. The process was ineffective. Our anchors had grown too close by the time I was locked away here."

"Aiolos, no," Andromeda interjects, "I promise you, killing me is the best chance we have of giving Lucy the life she deserves. This whole place will collapse. She won't remember me or anything I've done. Everything for her will go back to the moment we were orginally split."

+ _Kill Andromeda._
You raise your gun and point it at Andromeda, "Goodbye," you whisper.
You pull the trigger and the world around you collapses into a flash of white.
VAR KillAndromeda = true
~ FadeOutSeq("Thank you, Aiolos... / Please take care. / I put my full faith in you to stop this madness.", "JupiterCapita")
->DONE

+ _Kill Lucy._
You raise your gun and point it at Lucy, "I'm sorry," you whisper.
You pull the trigger and the world around you collapses into a flash of white.
VAR KillLucy = true
~ FadeOutSeq("Thank you, Aiolos... / I hope you carry on with dignity. / I hope you see me as an example that Jupiter is not a means to happiness. / It's a means to very destructive end.", "JupiterCapita")
->DONE
