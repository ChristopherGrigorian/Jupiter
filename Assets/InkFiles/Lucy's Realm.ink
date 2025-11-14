VAR FirstTimeOasis = true
VAR Drowning = true
VAR Seagul = 0
== Oasis ==
{FirstTimeOasis:
~ HideMapButton()
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
"No..." she says. "It's all just emptiness, I don't understand. Where's my other half?"
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
You look at Boris as if you might blow a fuse. He seems unphased by your bottled up anger. Andromeda can be heard giggling in the background.
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
"Not if we never decrypt the associated data..." she says, "and also encrypt the data currently circulating within us about drowning."
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
- else:

You all plunge through the surface of the water and dive deeper below. A limitless blue enshrouds you and the party.
}

{FirstTimeOasis:
"Ah, damn! It's the same thing as the beach. Everything just feel infinite around here. Where the fuck is Lucy?" says Boris.
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
-else:

+ _Head to the underwater train station._
    -> UnderwaterTrainStation
}

== WhaleFinish ==
Boris can't stop hyperventalating. Both Andromeda and you look to Boris. "Huuumph... Hummmph," Boris' heavy breathing continues.
Andromeda side-eyes Boris, "Boris... dude... breathe," she says. "Just re-encrypt the file on thalassophobia."
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
- else:

In this limitless blue, you stumble upon train station. The building is covered in mosaic like circle windows, through each you can see a multicolored shimmer seeping into the surrounding water. Train tracks sprout from either side of the building.

+ _Enter the train station._
-> TSHub
}

== UTSpecial ==
You grit your teeth in frustation, but let the whole thing go. You attention is drawn back to the building. It is covered in mosaic like circular windows. They are gorgeous and grand, the movements of the water glisten through and dance upon them. Through each you can see a multicolored shimmer seeping into the surrounding water, most probably due to a light coming from the inside. Train tracks sprout from either side of the building.

You swim towards the building and the party follows. 
You approach grand double doors at the front of the building. You reach the handle, only for Boris to shout at you for doing so.
"WON'T THE WATER GO IN? WAIT!"
You look to him all droopy eyed.
+ "There's only one way to find out."
- You reach for the door and throw it open. No water falls forward. The party walks through the entrance.
You all look behind you upon entering, only to see a wall of water standing tall by the door frame. 
"It... can't get in here?" utters Andromeda.
"I guess not..." Boris says while grasping at his clothes. "We're also... dry... What tech made that possible?"
"Not to stomp your curiosity, but that doesn't matter right now," says Andromeda. "Can we just focus on finding Lucy?"

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
-else: 

The room is grand in scale, and very barren. A few benches are sprawled about, and are made of the same material as the walls. If one squints, they would appear to be lumps that just swell from the floor. The room is split down the middle by the train tracks. The party stands together on the platform. A blinking sign can be seen hanging from the ceiling. A train is about to arrive. 

+ _Wait for the train._
    -> TrainWait
}
    
== Kiosk ==
The party approaches the kiosk.
{FirstTimeOasis:
"Oh, how lovely, a brochure," Boris picks it up and starts onfolding it.
"Oh... these are lovely," Andromeda chimes in as she begins carefully unfolding the brochure. "Very delicate though..."
"Oh wow," Boris returns. He turns the sprawled out brochure every which way, even upside down. "Yes... I see... hm... very interesting...yes... very nice..."

+ "What does it say bud?"
    ->Kiosk1
}

== Kiosk1 ==
Boris looks to you doe-eyed, "I don't know bud."
"I thought as much," you say.
You reach to grab the brochure out of Boris' hand but he pulls it away.
"Hey! Get your own man," he says.
You can see Andromeda radiating a light smile from the corner of your vision. You furrow your brows towards Boris.

+ "You're not reading it any way..."
- "The pictures are nice," he pouts.
"Fine..." you say.
You grab a brochure off the kiosk. You open it up and see that it actually doubles as a metro system, not of actual stops, but of writings similar to the note you found behind the door.
"This is location data, isn't it?" you ask while showing the page to Boris.
He peers over his own upside-down brochure and onto yours, "Yeah!" he says. "Would you like me to read it and send over the location data?"
"Sure, that would be great," you say. 
Boris begins analyzing the location data, and you see a tinge of confusion hit his eyes. 
Andromeda notices too. "Boris..?" she says.
+ "Is something the matter?"
- "No not necessarily," he mutters. "These zones just have strange names to them... they also come with danger warnings. I don't think we're cut out for any of these zones yet at our combat prowess, but I'll give them to you anyway. You can access them from your map feature while at a train station like this one."
Andromeda begins analyzing the location data provided from Boris. She seems to be in trance. "These places..." she says quietly. 
+ "Do you know of them from Lucy?"
- "The memories are fleeting, but I feel a lot of distress around their construction," she says. "There's no living person within this world... but there's a million ways to observe."
+ "What do you mean?"
"I don't know," she pauses. "It's just a feeling. Just think about it. Why is there infastructure here? Nobody lives here besides Lucy."
"Perhaps she just wishes to view her creations?" Boris says.
"Maybe it's a coping mechanism," Andromeda admits. "If I know of her, she most definetly knows the things I've done..."
Boris and you exchange glances before looking back to Andromeda.
"Sure..." Boris says uncomfortably, "For now though..." he continues, "let's just go wait for the train."
"Thanks for the guidance," you smirk.
"No problem, friend..." Boris smiles.
"Sarcasm may not be in your vocabulary, friend," Andromeda says.
Boris purses his lips, "If every time someone was nice to me I thought it was sarcasm, I'd be living a very... very boring life."
Andromeda ponders for a little, both in confusion and admiration, "I suppose you're right actually."
"Words to live by..." you murmur as you walk away from the kiosk.
The party heads back closer to the tracks to wait for the next train.
-> TrainWait

== TrainWait ==
{FirstTimeOasis:
"Where are we heading?" asks Andromeda.
"Wherever this train takes us," says Boris.
"Great plan..." mutters Andromeda.
"Well do you have a better plan?" snaps Boris.
"No," says Andromeda.
"WELL GOOD THING I'M THE BRAINS OF THIS WHOLE OPERATION!" Boris shouts.

+ "I wouldn't say that..."
Boris' eyes laser over to you.
"Just you watch," he says, "This is exacly how we're going to find Lucy."
You shrug in concession, "Alright..."
A gush of air begins to fill the chamber well before the train fullfills the space. You watch as carriages rush by and slowly screech to a halt before you. The doors open, inviting you within. The party enters.

"Wow... this is fancy..." says Boris.
An unfamiliar voice echoes through the cabin "I'm glad you think so..."
The party whips their head towards the voice.
A young woman is sat in one of the chairs. She looks upon you all with both grief and anger. 
Boris begins smiling, "Hi miss..." he's incredibly giddy with joy. "What's your name?"
"Lucy," she responds.
"YES! YES! FUCK YEAH I TOLD YOU BOTH," Boris shouts as he punches the air in triumph.
The woman's expression does not change as she looks from Boris to both you and Andromeda.
"I take it I'm who you're looking for," she says.
    -> TrainWait1
-else:

The train has arrived, please select a location from your map feature you wish it to take you.
}
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
Lucy inhales deeply, "I've only saw a glimpse of what we've done..." she says in calm. "This whole world is just a manifestation of my grief," she says. "Bruce abandoned me... and he still abandoned you..." she grits her teeth, "Yet, you still continue?"
Boris opens his mouth slightly as if he were wanting to interrupt, but he continues to remain silent.
"You weren't useful to him anymore, once you got a fucking heart from me," she says. "And while you got to live a fairy tale in his simulation, I was left here to just rot away and deal with what you've done."
Andromeda is completely flustered. You go to nudge her, but no words spill from her mouth.
Lucy remains poise, "Have you nothing to say at all?"
More silence ensues.
"Boris tries to break the ice, "This world is lovely, though, I hope you know that...," Boris looks around at everyone. Everyone's attention is on Boris. "I... um... haven't even seen some of these creatures before!"
"You've barely seen anything," Lucy snaps. "There's barely and life here."
Boris folds immediately, "You're right... sorry. There was a whale though."
Lucy rolls her eyes and returns her attention back to Andromeda, "You had ever possibility to make things right," she says. Lucy is simmering in anger. You feel it in every words she speaks. It seems as though she is about to burst. Her eyes pivot from Andromeda and meet yours. "You waited for one of Bruce's clones to show up before actually getting up and doing something?" she says.
Boris jumps in once more, "You are suprisingly calm... for having all these feelings held in for so long."
"I think I might just be at my breaking point," Lucy says. It seems she's about to cry.
Andromeda clenches her fists. Although a little late to addressing Lucy's point, she blurts it out as if she has finally pieced together what she wishes to say. "Bruce didn't abandon me!" Andromeda cries. "Aiolos is living proof of that."
"Oh spare me," Lucy says. "That's all you have to say? You have no clue what Aiolos desires."
"I'm..." Andromeda says. She stumbles on her words, "I'm going to stop it. Right here right now, you won't have to live with that grief once I'm gone."
"You don't know that," Lucy says. "You have no proof that one of our anchors will win over the other."
"You've thought of the same solution then..." Boris interrupts.
"Yes, I've thought of how stupid that sounds," she snarls, "And another thing, there you go again being selfish. What makes you think that you deserve to die over me?"
"I don't... I just wanted to be able to give you your spark back..." Andromeda whispers.
"We both know we lost it long ago," she says. "The promise of mass happiness becomes so lackluster once you realize its cost."
Lucy calms down a bit, "I shouldn't be mad at you. I'm sorry. You're just the part of me I wish I could forget."
Before anyone in the party can answer, the train car begins to rapidly fill with water.
Boris looks towards his submerged legs in panic, "Not more swimming!" he cries.
The water continues to rise. 
Lucy begins to manifest an umbrella within her hand and opens it up. It's opaque and reflects muted colors onto the nearby water.
The whole cabin is now completely submerged.
"Where are you doing?" Andromeda demands.
"Riding the train," Lucy responds, "I don't owe any of you anything."
The sound of metal screeching and bending can be heard all around. The cabins begin to fold onto each other forming a solid water tube as far as the eye can see.
"I'll see you all around..." Lucy mumbles. She tilts her umbrella and catches a current, flowing down the tube.
Boris reaches his hand out as Lucy vanishes into the distance.
Andromeda looks to Boris and starts shouting, "What were you going to do? Grab her? Put your hand down!"
"Hey jeez," Boris defends, "Don't be mad at me, I'm not the one that got Lucy in this situation in the first place."
Andromeda begins hyperventalating.
"Andromeda, dude, breathe," he smirks.
"Shut up!" Andromeda snarls.

+ "Let's follow Lucy."
    ->LL1
+ "Stop fighting, let's get going."
    ->LL1
    
== LL1 ==
Boris stands at attention and looks to you, "We don't have umbrellas to ride the current."
"Just... manifest one?" Andromeda says. "You have the fucking data."
Boris and you exchange glances like you had once before.
"Okay..." Boris whispers.
The party manifests umbrellas and rides the current in pursuit of Lucy.
The party whirs through the train like tube.
"Whoo! This is actually pretty fun!" Boris shouts.
"Yeah," Andromeda smiles. "It really is."
The party looks through the windows of the tube as they flash by. Smears of different sorts of fish and parts of larger species can be seen briefly.
An automated voice can be heard ringing all around, "Thank you for traveling with us... The next stop is... The next stop is... The next stop is..."
"THE NEXT STOP IS WHAT!" Boris shouts.
The cabin begins to rumble. The current ceases to flow. The party is once more grounded in the flooded cabin.
Andromeda looks widly around, "I think we're being attacked..."
A wild metal banging can be heard on the outside of the cannon. Bits of the roofs collapse and smaller fish begin to seep into the cabin.
"Something big is coming!" Boris cries. 
The roof completely collapses and a massive shark forces itself in front of the party. It begins brandishing its teeth. Two jellyfish also infiltrate the scene.
"These don't look friendly!" Andromeda warns. "Prepare for combat!"

~ StartCombat("SharkAttack", "LL2")
->DONE

== LL2 ==
Boris begins brushing every part of his body. "I think they got me! I think they got me! Those things definetly stung me bad!" he cries.
"A tad bit dramatic, huh?" Andromeda says.
"You don't get to talk about drama after that scene with Lucy. At least when I'm hurt I'm able to say something," Boris retaliates.
"How would you ever know what I was feeling in that moment you stupid bird," she snarls. "You don't even know a fragment of the guilt I feel."
Boris grits his teeth but restrains himself, "First off... I'm not a bird... Second, don't say this me, say it to Lucy." 
"I will... okay? I will we just need to find her first alright. Just help me," Andromeda says exasperated.
Boris is tranquil now, "Thats all you have to say..." He looks to you now. "Ready to continue our pursuit."

+ "Yes."
"Great," he says. "March, man... Or float, I guess," he stumbles as he manifests an umbrella once more.
-> LL3
+ "No."
"Well that's too damn bad," he says. "March, man... Or float, I guess," he stumbles as he manifests an umbrella once more.
-> LL3

== LL3 ==
The party continues to ride the current until it dissapates once more. The voice comes back on the speaker, "Final stop on this line. Welcome to Burning World. Please exit upon the chamber being drained of water."

The party is on standby as the water level sinks down once more, emptying into grated corners of the train carriage. 
"So much for a train station, what was the point of it pulling up to the station if we're the one's transporting through it?" Boris pouts.
Andromeda looks over and just shrugs, "Perhaps an art of sorts. Maybe it has greater meaning with Lucy."
The train doors open and the party exits into another station identical to the last. Lucy is nowhere to be seen within the station.

+ _Exit out of the station._
-> LL4

== LL4 == 
The party exits the station. Everyone is once again submerged in water. It seems as though you're at the bottom of the ocean. A cathedral pulses in the distance. It burns without end, even though it's submerged in water.

The party approaches and the cathedral swells into sight. Lucy's figure can be seen stood before the building. Fragments scorch and jump off of the building. The cathedral is closely resemblant of the chateau's detailing and structure.

Lucy hears you all approach. She turns to meet you all. She still holds her umbrella tight, holes are scorched into fabric from bits flying off the house.
Lucy's face drops with sadness, "Why did you follow me?" 
The party is stunned by the image of the burning cathedral. An uncomfortable silence ensues. 
"The building is beautiful isn't it?" Lucy says. "It burns me, even through this umbrella. I'm so far away, yet it haunts me. It's breached the barrier, and now I can't get enough of its sting."
Lucy looks to Andromeda now.
Andromeda tries to speak up, "I know you loathe me but-"
"I loathe and love you," Lucy interrupts. "You're both a blessing and a burden. Imagine being so delusional to the fruits of your own existence."
Lucy furrows her brows, "I know exactly who I am. You, Andromeda, are still back peddaling. Fix yourself before you go out of your way trying to fix me. 
"I'm not trying to fix you, I'm trying to help you," Andromeda reasons. 
Lucy looks to Andromeda in anger, "You're not trying to help. You just want freedom from yourself and what you've done, no matter if death is the most tangible answer."
"I-" Andromeda blurts.
"I know you" Lucy interrupts again, "I know us. This is selfish, and you know it. I'll take care of Aiolos myself, even if you won't."
Seaweed and tentacles burst from the ground and restrict Andromeda's movement.
Lucy is in mania, she grits her teeth, "I'll finish the job you should have done the moment you laid eyes on him."
"NO! Andromeda shouts. "We need him! I promise you! Don't fuck this up!"
Lucy just shakes her head, "I wish I could trust you in this moment. Just give me a moment, and we'll talk about this after."
Lucy is completely enraptured and out of commision.
"Come forth," Lucy points to you and Boris, "I'll finish the job."
Boris starts shaking, "Oh shit man, I don't think she's joking... Prepare for combat."

~ StartCombat("LucyFight", "LL5")
-> DONE

== LL5 ==
Lucy collapses to the floor. She begins struggling. "No... I won't be embarrased any longer," she cries. "Get out of my realm and just leave me alone," she mumbles.
She starts running away back to the train station. The restrictions around Andromeda drop. The party goes after her. You see Lucy rushing back into the train car.

"No! You're not running away this time," Andromeda yells while chasing her down into the carriage. "Please, I promise, this is what's best for everybody. If I'm out the picture you'll finally be happy!"

Lucy stands firmly planted within the carriage and turns around. She completely ignores Andromeda and looks to you. "You know what..." she mumbles to herself.

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
