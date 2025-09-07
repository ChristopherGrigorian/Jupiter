== Rabbit ==
~ PlaySong("InBetween")
You don’t know how long you’ve been sleeping, but you wake up with your face in some concrete. 
You’re on an empty street. You manage the strength to lift yourself onto your knees. 
Your vision is a little blurry, but your body is seemingly intact. You look behind you in search of the apartment from which you’ve thrown yourself. 
There’s no such place behind you. You feel like you’re nowhere. 
The world echoes blue and indigo all around you as if the colors were whispering in the air. You inhale, and the world around you swirls into your lungs. 
There's a horrible beeping permeating all around you. 
You slowly turn round again, only to be met with a slender figure placing their face uncomfortably close to yours. 
They’re crouched on the floor before you. They sport a suit and are humanoid in figure. They're covering their face with a rabbit mask.

+ "Who are you?"
    -> RabbitBrain
    
== RabbitBrain ==
"Who are you?" you ask, uncertainly. The figure looks at you with curiosity while lifting itself to a half squat. 
“Rabbit,” it responds quietly.

+ "Rabbit?"
    -> RabbitBrain1
    
== RabbitBrain1 ==
“Rabbit, huh. I’m Aiolos,” you announce. You’re not sure whether to feel scared or not. 
"Can you do this?" Rabbit asks. He holds out his palm, and within it, you see a revolver begin to materialize. It's radiating a brilliant light blue color. 
"Um.. No," you respond. 
"Soul weapon," Rabbit says. "Think, and behold."
"Think and behold," you echo.
Rabbit looks at you, confused. "Do, don't say," they instruct.
You extend you arm out and look at your palm.
You focus, and think of a revolver in your mind--it's shape, feel, and power. You pull this image into your hand, attempting to will it into existence.
Sure enough a radiating blue gun materializes in your hand.
"Bravo," Rabbit announces. You drop your focus, and the gun disappears. 
Rabbit nods to you, gets up, and starts walking away. You feel compelled to follow Rabbit.  

+ _Follow Rabbit_
{BornAgain > 0 && KillCommandAlmos == true:
    -> DHR
- else:
    -> FollowRabbit
}

+ _Run away from Rabbit_
{BornAgain > 0 && KillCommandAlmos == true:
    -> RotAway
- else:
    -> RunAway
}
    
    
== DHR ==
You approach Rabbit, but as soon as  you start following, they turn around and shake their head disapprovingly. You feel an overwhelming urge to run away from Rabbit.
-> RotAway

== FollowRabbit ==
You cautiously follow Rabbit. Their footsteps sugar through the blue and indigo mist surrounding you. Their silhouette promenades at the edge of your vision. 

You follow them for a while. Their footsteps ramp, faster and faster. To keep them in vision, you’re brought to a jog, and then burst into full-on sprinting.

{BornAgain == 0:
    You lose Rabbit in the mist, you're utterly alone and swallowed in fog. A beeping noise continues to ring on in the distance. You hear yourself getting closer and closer to the source. 
    You're movements become a lot more deliberate and cautious. 
    You walk aimlessly in the genral direction of the noise until you stumble upon a figure in the distance.
    "Rabbit?" you call out. 
    No response.
    You approach thinking it's Rabbit, but you're greeted with a mechanical figure. They are faceless, and the metal rods and cogs that compose their body turn to you. 
    You feel the figure approach you with malicious content. You feel the need to protect yourself. You once more materialize a revolver in your hand.
    
    The figure clanks towards you. Prepare for combat.
    ~ AddWeapon("Revolver")
    ~ PlaySong("FacelessConstruct")
    ~ StartCombat("FacelessConstruct", "FollowRabbit1")

- else:
    You lose Rabbit in the mist, you're utterly alone and swallowed in fog. You begin to walk aimlessly and stumble upon a figure in the distance. You approach thinking it's Rabbit, but you're greeted with a balloon animal. You feel the most viscious of auras radiating off of it. It's most definetely dangerous. You tense yourself, and prepare to eliminate the balloon animal. You once more materialize a revolver in your hand.
    
    The ballon animals waddles towards you, ready for a fight. Prepare for combat.
    ~ PlaySong("BalloonAnimal")
    ~ StartCombat("BalloonAnimal", "FollowRabbit1")
}
->DONE

== FollowRabbit1 ==
~ StopSong()
{BornOnceAgain == 0:
    The construct's gears sputter and grind to a halt.
    Its final twitch echoes in the silence. You're still breathing, but barely.
 - else: The balloon pops and deflates. The final bits of air that substantiated its being whir into the wash of colors that encompass this world.
}
-> FollowRabbit2

== FollowRabbit2 == 
You're not sure what's become of you or how you've just performed such feats. It felt as though something within you has awakened. You dread no longer on the topic, and continue your search to find Rabbit, and the source of the beeping noise.

You continue to follow the noise until its blaringly close by. 
You're seemingly endless wander is interupted once more by another silhouette. When you approach, you realize it's Rabbit again. The figure stands next to a cubicle, and beeping, object sprouting from the floor. On further inspection, you realize it's a phone box. 

"Call," beckons Rabbit. You only know your own number.

+ _Call yourself._
    -> R1

== R1 ==
{PhonePickup == true:
    You would think it would be pointess to call yourself, but you realize you don't have your phone in your pocket. You remember you left it out on the table to see whether aluminum could rust.
    - else:
    You would think it would be pointless to call yourself, but you realize you don't have your phone in your pocket. You thought for sure it was in your pocket from when you jumped off the balcony, but when you go to feel for it, it isn't there at all.
}

You call your own number, and the phone rings.

{PhonePickup == true:
    Someone picks up on the other end. You hear some shuffling before a voice indistinguishable from your own greets you with a "Hello? Who is this?"
    Rabbit looks upon you; its body language is heavy with grief.
    
    "This is Aiolos... who are you?" you ask.
    
    "I'm Aiolos too," the voice responds. They sound hollow, "you sound exactly like me."
    
    I think we're one and the same," you reply. "Are you on the balcony right now?"
    
    A weary "Yes...?" crinkles across the line. You look to Rabbit for even the slightest bit of clarification. They continue to be mute. Their eyes pierce through yours. You feel as though they are artificially stringing fish hooks through your eyes and poking and pulling at your throat. You feel compelled to say just one thing.
    
    "Kill Almos."
    
    + _Continue..._
        ~ KillCommandAlmos = true
        ->BornBefore
    - else:
    Rabbit begins ringing. There's a glowing square in their pocket. Rabbit picks up your call. Their eyes pierce through yours. You feel as though they are artificially stringing fish hooks through your eyes and poking and pulling at your throat. You feel compelled to say just one thing.
    
    "Kill Aiolos."
    
    Rabbit approaches you, and in your panic, you collapse  to the floor and blackout. Rabbit kills you, and you unravel into nothing at all.
    
    + _Continue..._
        ~ FadeOutSeq(" ", "BornOnceAgain")
        ->DONE
}

== BornBefore ==
~ FadeOutSeq("Jupiter is prolific. / Jupiter is demanding. / Jupiter is the Absolute Authority. / You are now playing as Almos.", "TheGarden")
->DONE

== BornOnceAgain ==
You feel your vision go hazy for a moment, but you deem it a passing moment of vertigo. What were you about to say again? Oh, yes, you remember now.

{JMP1()}

Your thoughts are interrupted by your phone ringing in your pocket. You pick up the phone. "Hello? Who is this?" You hear nobody on the other end. Wrong number, you suppose. Since your phone is out anyway, you might as well search if aluminum can rust.

No. Aluminum doesn't rust, apparently. It corrodes. Huh. Interesting, you suppose. You leave the phone out on the table.

You try getting Almos' attention again by touching his hand, but he stays fixated on the street below. He gets  enraptured like this sometimes. Perhaps he's daydreaming. Maybe he isn't even really there. His body is before you, but his mind is on another planet, for all you know. You sit in silence for a bit longer.

+ _Daydream along Almos._
    ~ PhonePickup = true
    ~ BornAgain += 1
    -> Daydream
    
== RotAway ==
You wander aimless through this picturesque orchid-hued world. You check behind yourself occasionally, and Rabbit is nowhere to be seen. You suppose he didn't care much to follow you, only lead. 

A blue and indigo mist continues to swirl around your legs as you walk though each empty street after the next. You finally stumble upon a park, within it stands firm a children's slide. A peculiar figure could be seen sliding down it, and promptly climbing the ladder attached over and over again. You approach slowly, and catch the figure's attention. They leave the slide and walk up to you.

{MeetBoris > 0:
"Hellooo again," he bellows.
"Again?" you prompt.
"Well, this version of you won't remember me, haha. But rest assured we have met before. There are a million spindles of you about, and I'm sure to meet a million more."
He grabs hold of your arm and pulls you close to him. With the other hand he clamps beneath your chin and examines your face.
 - else:
 "Hellooo," he bellows. "Look at what we have here!" He grabs hold of your arm and pulls you close to him. With the other hand he clamps beneath your chin and examines your face. 
 }
 
 * "Mind getting your hands off of me?"
- He retracts a little and just smiles. "Hahaha, aren't you a sexy one. You're diseased, aren't you? You look sickly and beautiful and empty." His face grows wicked and stupid. "Jumping off a balcony for a man, now? How repulsing."
 
 * "It wasn't like that, you know. Something called to me."
 - "Ah, yes, of course," he says while pacing about. He circles and examines you now like a shark teasing its prey. He's dressed in gray fabrics as if someone wrapped a feathery tapestry around him. The clothes carry the occasionally smear of green hues. 
 
 {UncannyPeculiarity > 0:
"Also, who are you calling diseased?" you spout. You're the one that looks like a damn pigeon."
"Perhaps I was in another life, who knows." he responds. "I certainly dress the part, don't I? Anyhow..."
}

He halts in front of you once more, "That would be Jupiter that was calling to you." 
"Jupiter? I was looking out at the moon," you say.
"It was lovely, wasn't it?" the man says. It seems like the man just ignored what you said. 
"It takes many shapes and forms," he continues. "Jupiter is multitude, NO, an AMALGAMATION of inexplicable feeling. 'THE PINACLE OF EXISTENCE' some might be bold enough to say." He pauses briefly to clear his throat, "It's also a very real place."

* "Are we on Jupiter now?"
- "Of course not," he responds dully. "We're in a sort of in-between world. A proxy, if you will. Your signal to Jupiter was interrupted by something. This place is just as artificial as the reality you once stood in before."

* "So, who interrupted the signal?"
- "Well, that little friend of yours did," he says cheerily. "I forgot to mention though, I'm Boris."

You think Boris is a bitch.

"Well you're awfully quiet now. It's nice to meet you," he strikes his hand out.

You gaze at his hand, but don't go to shake it. You return your focus to his face.

* "Why the hell are you here?"
- "Straight to the point, are we? Well, according to the script, I'm here to keep you on track when you make stupid decisions. Rabbit is one of Almos' creations."

You interject, "Didn't you just insult me earlier for jumping off the balcony? Why would Almos create you to say that?"

"Perhaps he thinks you're predicatable, or just straight pathetic. Who knows, I'm no prophet! Haha! I'm just following the script. Anyway, I'm glad you're here now. Almos wants you to enter some building over here." 

<i>Boris promenades before you leading the path. Before long, you stumble upon what looks like government facility. You feel compelled to voice this to Boris.<i>

"Hey, isn't this a government building?" you question.

Boris reels and is taken aback, "How would you know what a government building looks like? Did... someone tell you that this is a government building? You're a spooky one, you know that? Anyway here it is. Almos awaits you within."

Boris reaches into his pocket and pulls out a keycard, opens the door, and beckons you through. 
    ~ MeetBoris += 1
    
* _Enter the building._
-> Government

== RunAway ==
You wander aimless through this picturesque orchid-hued world. You check behind yourself occasionally, and Rabbit is nowhere to be seen. You suppose he didn't care much to follow you, only lead. 

A blue and indigo mist continues to swirl around your legs as you walk though each empty street after the next. You finally stumble upon a park, within it stands firm a children's slide. A peculiar figure could be seen sliding down it, and promptly climbing the ladder attached once again over and over again. You approach slowly, and the figure catches you approaching. He leaves the slide and walk up to you.

"Hellooo," he bellows. "Look at what we have here!" He grabs hold of your arm and pulls you close to him. With the other hand he clamps beneath your chin and examines your face.

* "Mind getting your hands off of me?"
- He retracts a little and just smiles. "Hahaha, aren't you a sexy one. You're diseased, aren't you? You look sickly and beautiful and empty." His face grows wicked and stupid. "Jumping off a balcony for a man, now? How repulsing."
 
 * "It wasn't like that, you know. Something called to me."
 - "Ah, yes, of course," he says while pacing about. He circles and examines you now like a shark teasing its prey. He's dressed in gray cloths as if someone wrapped a feathery tapestry around him. The clothes carry the occasionally smear of green hues. 
 
 He halts in front of you once more, "That would be Jupiter that was calling to you."
 "Jupiter? I was looking out at the moon," you say."
 "It was lovely, wasn't it?" the man says. It seems like the man just ignored what you said. 
 "It takes many shapes and forms," he continues. "Jupiter is multitude, NO, an AMALGAMATION of inexplicable feeling. 'THE PINACLE OF EXISTENCE' some might be bold enough to say." He pauses briefly to clear his throat, "It's also a very real place."
 
 * "Are we on Jupiter now?"
- "Of course not," he responds dully. "We're in a sort of in-between world. A proxy, if you will. Your signal to Jupiter was interrupted by something. This place is just as artificial as the reality you once stood in before."

* "So, who interrupted the signal?"
- "Well, that little friend of yours did," he says cheerily. "I forgot to mention though, I'm Boris."

You think Boris is a bitch.

"Well you're awfully quiet now. It's nice to meet you," he strikes his hand out.

You gaze at his hand, but don't go to shake  it. You return your focus to his face.

* "Why the hell are you here?"
- "Straight to the point, are we? Well, according to the script, I'm here to keep you on track when you make stupid decisions. Rabbit is one of Almos' creations."

You interject, "Didn't you just insult me earlier for jumping off the balcony? Why would Almos create you to say that?"

"Perhaps he thinks you're predicatable, or just straight pathetic. Who knows, I'm no prophet! Haha! I'm just following the script.

Boris is now looking off in the distance, "You hear that beeping right? It's best you follow it. It's getting kind of annoying now, ain't it? Also, isn't this your lucky day, I see Rabbit right over there in the distance, I think they still want you to follow them."

    ~ MeetBoris += 1
    
* _Follow Rabbit._
-> FollowRabbit