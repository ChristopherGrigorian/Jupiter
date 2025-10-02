VAR SpokeToWoman = false
VAR TTBAC = false
VAR BorisAmnesia = false

= Galagos 
{FirstTimeGalaga:
    You feel yourself being stripped away from reality. 
    You unravel over and over again. You are woven whole. The image of the condos and chateau materialize before you. You are standing right before the courtyard leading to the chateau. 
    This whole place feels earily...Earthly. No harsh wind, no flimsy floor. A bright blue sky rests behind the chateau. You hear birds chirping, the air feels crisp and wonderful.
    ~ FirstTimeGalaga = false
    + _Look around for Boris._
        ->LeftOfFront
    + _Explore on your own._
        ->FrontOfCourtyard
- else:
    This is Galaga.
    ->FrontOfCourtyard
}

== Courtyard ==
The courtyard is very grandiose and breathtaking. Statues of angels and fountains are positioned symmetrically on either side of the walkway heading to the chateau.
You see a wandering merchant here. 
{MetAnzhela == 0:
~ MetAnzhela += 1
She approaches you.
"Hello there, wanderer! I'm just so giddy finally...FINALLY... I have a new customer."
The merchant can barely keep their balance. They sway back and forth with the weight of their backpack.
You say nothing.
"Ohhh... I see you're one of those shy ones," they say. "Name is Anzhela by the way!" 
Anzhela throws her bag to the floor and unwraps it into a nice tarp. A couple of items roll about on top of the tarp, street-vendor style.
"Neat, huh? A backback that is also a tarp, and also a blanket...and also potentially a tablecloth... and... and..."
If you don't interupt Anzhela now, she'll just go on forever.
+ "Could I take a look at your goods?"
    "YES OF COURSE," Anzhela begins coughing in a wicked fit. 
    She stablizes, "That's why I rolled it out, silly. Here! take a look!"
    -> AnzhelaShop
- else:   
"Would you like to visit their shop?
+  Enter Anzhela's Shop
    -> AnzhelaShop
+ _Enter the chateau._
    {TTBAC:
    ->Chateau
    - else:
    ->ANZWAR
    }
+ _Leave the courtyard._
    ->FrontOfCourtyard
}

== ANZWAR ==
"Woah woah woah!" shouts Anzhela. "I wouldn't head in there traveler, it's been overrun by rogue inventions! It's far to dangerous."
You heed Anzhela's warning, and don't proceed to the chateau.
->Courtyard
== LeftOfFront ==
You start walking to your left, and see Boris eyeing you from a bench.
+ _Head over to Boris._
    ->TTB
+ _Turn Around_
    ->FrontOfCourtyard
    
== FrontOfCourtyard ==
You are standing in front the courtyard that leads to the chateau. You can see large and immaculately maintained hedges, and canaopy welcoming you into the courtyard.
There is also both a pathway to your left and right that walk along the perimeter of the courtyard.
+ _Enter the courtyard._
    ->Courtyard
+ _Go down the left path._
    ->LeftOfFront
+ _Go down the right path._
    ->RightOfFront
    
== RightOfFront ==
You walk along the perimeter, guided by tall hedges that encapsulate the courtyard. A lone lamp post stands firm in the ground at this corner. A bench rests under it.
+ _Sit on the bench_
- You sit on the bench. It feels uncomfortable. You get up.
+ _Turn the corner_
    ->Condos

== Condos ==
You're met with a small strip of colorful condos. A woman can be seen resting on one of the porches.
+ _Talk to the woman._
    ->TTW
+ _Turn around._
    You turn around and head back around the corner once more. 
    -> FrontOfCourtyard
    
== TTW ==
{SpokeToWoman == false:
You approach the woman.
~ SpokeToWoman = true
+ "Hello."
-> TTWCONT
- else:
    {FirstTimeInChateau == false:
        The woman wishes not to speak with you.
        ->Condos
    - else:
        Boris trails behind you, cowering slightly in your shadow.
        ->AndromedaConvo
    }
}

== AndromedaConvo ==
"Why do you insist on bugging me?" she snaps.

+ "Boris said to."
Andromeda sighs, "You both were and still are insufferable."
"Why are you not telling me everything? Why are you being so mysterious about everything?" you say.
"I don't know ANY of your intentions. You don't remember anything. I can't tell you who to be," she says.
"Well, could you tell me who I once was?" you rebuttal.
-> AC1
+ "You're not telling me everything."
~ BorisAmnesia = true
"Well, nor has Boris. Not that it's on purpose, he just has a sort of amnesia like you."
"Wait what?" Boris chimes in.
Andromeda rolls her eyes.
-> AC1

== AC1 ==
"Well... what has Boris told you so far?" she asks.

+ "That we're some alien race of freaks that kill people."
- "Okay, that's a good start, I guess," she says. 
+ "That I've trapped something behind doors."
- "That's true..." she ponders.
+ "He told me about the Telepolos."
- "... alright" Andromeda says.

-> AC2

== AC2 ==
"Let's just start broad," she says.
{BorisAmnesia:
"Wait! Why do I have amnesia?" Boris interrupts.
"Shut up Boris," you snap.
He looks at you wide-eyed then back to Andromeda.
}
"Boris told you about how we're sort of a militia race, right?"
"Yes," you nod.
"What do you think makes a strong fighter?"

+ "Strong weapons."
->AC3
+ "Strong skills."
->AC3
+ "A strong team."
->AC3

== AC3 ==
"Okay, how about an elite sort of fighter? One to literally crush universes, planets, and the livelihoods of millions of cilivilizations?"
You and Boris look to her, both empty and expressionless.
"It's a lack of humanity, but with the presence of a sort of essence that makes us real."
+ "A soul?"
- "Something similar. We call it a Continuum Anchor. Because we have once existed as living, thinking, and feeling biological entities, we are far more grounded in reality." 
"We are not subject to blur or fragment as easily when encountering resonance weapons. They are the most destructive tools in our universe. There is no conceivable defense against a person's will manifest. Your personhood is therefore, a very potent powersource."

+ "What about having a lack of humanity?"
->AC4
+ Nod quietly.
->AC4

== AC4 ==
"Conscious splitting, or memory splitting, is how we artificially gain a lack of humanity," she says. "Everyone with a Continuum Anchor can manifest resonance weapons, but how willing would you be to turn your weapon on another living creature?"

+ "I would do it in a heartbeat."
- "I hope you mean in self defense... Perhaps you aren't much different from your old self. Anyway..."

+ "I would never."
- "Such would be the sane response."

-> AC5

== AC5 ==
"The 'human' part of you would persist in one entity, and the other would persist in an avatar. Your Continuum Anchor would be split, and you could live two completely separate lives. One focused on a hedonistic, everlasting existence with every imaginable experience in the conveivable universe, and the other a weapon of mass destruction."

"Am I a weapon of mass destruction?" Boris asks.
"No, you are an experiment gone wrong... or right depending on how you look at it," she says.
"Oh..." Boris says.
Andromeda looks to you now, all smug, "Aiolos isn't even your real name, by the way."

+ "Then what is it?"
- "It's Bruce," she murmurs.
"Bruce was able to create an aritifical Continuum Anchor and instill it in you, Boris. Because of that, he was able to conscious split you. Your combat double is Rabbit."

You look to Boris. You've been presented with quite the bombshell. There's been many such cases of you not being able to pinpoint your emotions, but this time, you feel scared.

"After Boris, though," she continues, "you never attempted the procedure again. Perhaps Boris was a wake up call of sorts, a coupe de grace to prove that you could actually do it. Maybe you thought we took it too far."

-> AC6

== AC6 ==
* "Are you the combat double, Andromeda?"
- "Yes, I am," she sighs. 
"The thing about conscious splitting though is that it doesn't work. The Continuum Anchor has a means, or rather a desire, of maintaining a connection with itself. You could split it over and over again, but all the pieces will slowly merge back with one another."
"When they collide though, we can only speculate what would happen. Presumably the anchors would have experienced far too different experiences to capture them in one person again. We don't know if they would merge, or one conscious would override the other."
"I can only speak from experience though. That slow walk of the anchor back to itself brings a little bit of humanity back to a weapon like me over time, and probably a lot of guilt to the counterpart. Despite me having traces of my other half, on combination, another clean split may occur, where one half gets completely erased." 
"The solution was to seal the other half in such a way where the anchor couldn't make it back to itself. My other half was sealed far too late. I lost all desire to continue this rampage."
-> AC7

* "Am I the combat double to Bruce?"
"I'm not sure what happened after your capture, so I can't say for sure. You were a tool of this universe and nothing more. You went rogue, and that's why they severed you from this place."
->AC6


== AC7 ==
+ "Why was your other half sealed so late?"
- "We didn't know any better at the time. I was one of the first to undergo conscious splitting."
"It was just sort of a side effect neither of us expected..."

+ "Does the collision happen naturally over time?"
- "Precisely," affirms Andromeda. 
Boris sort of picks at this beard awkwardly, "So... let's just break the sealed version out and let time take care of the rest, right? That way the killings stop, but Jupiter still remains. Nobody is capable of creating more of these elite soldiers besides Bruce."
"It's not that simple," Andromeda says. "For the victim of the seal, there's only one way in, and there's no way out. Based on our lack of knowledge, there may not even be a practical reason to actually enter one." 
"Besides, they're extremely dangerous to enter. We have no idea how the pysche of those sealed are holding up. Those places have become their exclusive universes. Anything goes according to their desires."

+ "Could we go straight for the elites?"
"Even as a team, we would have no chance," she says. "Unlike me, the other elites still have their full will to fight and conquer."
-> AC8

== AC8 ==
+ "So what do we do?"
-> AC9
+ "This feels like a losing game."
-> AC9

== AC9 ==
"You want my help, I presume? Let me help you help yourself. Consider me as the experiment. We go into the simulation world and find my other half. It's part selfish, but also part valuable."

"That's it? And then?"

"And then you kill me, Andromeda, and observe what happens to be other half."

+ "What? No."
"Don't be too hasty," warns Boris. "Just think about it. It may be worth a shot."
-> A10

+ _Look to Boris._ 
You look to Boris and he just shrugs.
"I mean..." he starts, "It's worth a shot I suppose."
-> A10

== A10 ==
"I'll lay it out for you," Boris says. "There's literally two possibilities."
"If we kill Andromeda there, her memories could combine with her other half. In which case we'll still have a grief filled being that doesn't have a desire to kill others." 
"Or..." Boris wrings his hands in anxiety, "We're going to watch a pretty nasty battle between two psyches and we'll get to observe which version wins." 
"If things get rough, we just leave the simulation, and if Andromeda, or their other half, goes crazy they'll be stuck there in the simulation."

Boris explanation helped make you a little more rational in the thought process.
You look to Andromeda.
+ "Are you sure you're okay with this?"
"Yeah, absolutely, it's all for the betterment of this universe. I've always wanted to meet with my other half anyway..."
->A11

== A11 ==
+ "I take it you'll be joining us then?"
"Yes, my skills are at your disposal. Just note though that since the bridge between my other half had started forming, I'm not as strong as I once was. I won't be a burden, though."
Andromeda joins your party.
This would be the next point in the story, but I haven't written anything yet. Thank you for playing.
->DONE

== TTWCONT ==
The woman looks to you, "You were found and outcasted, I take it? Suprised to see you back here."
"What exactly do you mean?" you ask.
"You have no fake flesh - which probbaly means no memories of this place, I assume," she explains.
"Oh... well yeah," you respond.
"Is that Boris that's traveling with you? I thought I saw an identification code that was familiar when you both joined the instance. He's a peculiar fellow, isn't he?"
"I'm well aware," you say. 

* "I'm less familiar with why you're here, though."
- "I'm not sure how that's any of your business. People like you have already caused enough damage, I don't need to answer to you," the woman says.
<i>The damage you've caused? You can't really come to terms with it at the moment.<i>
"May I ask just one thing, then," you query the woman, "Are you of the same mind as Boris?"
She looks to you with a hint of disgust, "No, we are not the same, I am not your creation, Aiolos. I am, and used to be, a lot of things, however."
"A ruthless killer, a beacon of hope, a product of demise. I'll surely have the same fate as you if I ever left this place."
* "Why?"
- "I once shared the same virtues as the man that built Boris. Those dreams have since been snuffed out. I'm scared of what they'll do to me." 
* "Would you join me and help me once more?"
- She looks to you in confusion, "I had loyaltly to one person only, you are him no longer."
{FirstTimeInChateau == false:
    The woman no longer wishes to speak to you. You get off of her porch.
- else:
    The woman seems to shoo you away, but you still feel somewhat determined to speak with her, as per Boris' request.
}
->Condos

== AnzhelaShop ==
~ OpenShop("Anzhela's Shop", "Courtyard")
->DONE

== TTB ==
"Finally decided to talk to little ol' me?" Boris snickers, "I'm flattered, really."
Boris stands up from the bench. "So, how do you like this place?"

+ "It's nice, I guess."
"Is that so? I sense a bit of doubt," he says.
    ->TTB1
+ "A little over the top for my tastes."
"Presumptious as ever, perhaps some things truly never change about a person, even after severance."
    ->TTB1
    
== TTB1 ==
You're about to interject, but Boris interupts you.
"Have you met the residents of this place yet? Andromeda is a particularly fierce character... "
"I'm actually quite afraid of her," Boris says while wringing his hands.

{SpokeToWoman:
+ "I've spoken to her. I'm not sure she wishes to speak with with me any longer, though."
    Boris sighs, "Oh, as unforgiving as ever. I hope you don't think her rude... she's just, wounded, I would say."
    ->TTB2
- else:
+ "No, I haven't spoken to her yet."
    "No matter," says Boris, "You may end up saying the wrong things, I'm not sure she'd appreciate seeing you like this, anyway."
    ->TTB2
}

== TTB2 ==
"Yeah...um" you mutter. You get a little choked up. You don't feel yourself to be sane. Everything is just so foreign, and upsetting, actually. 
Boris senses your discomfort, "You must feel pretty overwhelmed right now," Boris says. "Rest assured, while you may have forgotten everything, I have never forgotten the mission we set out on! I'm here to guide you, every step of the way."
Boris let's out one of his atrocious laughs again, "Consider me your rock, even though you're probably going to be the one that's holding me back!"
You think Boris is a bitch.
"Anyway..." says Boris, "In order for you not to be the rock in this situation, we kind of have to up your combat prowess. The people we're messing with are no joke. Luckily, the chateau is overrun with monsters at the moment."
"It's been a while since I've done some cleaning, but now that you're here we can do some chore-," Boris stutters, "I mean combat together."

+ "How has the chateau become like this?"
- "Well..." Boris says, "Some of your creations sort of went rogue a while back. We sealed them in the chateau for now."

+ "Why wait until now to deal with the problem?"
- "Mostly laziness," says Boris, "Besides, why's that important at this point? You may be able to rekindle some memories if you see the inside."
The prospect of regaining some of your memories has peaked your interest in pursuing the chateau. You know nothing about these 'creations' that Boris speaks of, much less that he claims they belong to you. You hold these thoughts close by as you set your sights on the chateau.

+ "Alright, let's get going."
    ~ TTBAC = true
    -> TTB3
    
== TTB3 ==
"Great!" chimes Boris. "Before we get going, I should let you know about upgrading your weapon. When you continuously run into visions of weapons, your familiarity with them becomes enhanced, and therby increases their power. Most weapons even gain new skills when they get upgraded!"
Walk with me, Boris gestures. You walk with him towards the front of the courtyard. 
"Anzhela, the merchant in the courtyard, will sell you a vision, or rather data, about specific weapons," Boris says, "I think she has a revolver for sale. If you haven't already, you should really buy that from her to enhance your ability to manifest your revolver."

+ "Understood."
- "Great, I'll leave you here, meet me at the chateau when you're ready."
->FrontOfCourtyard

== Chateau ==
Boris is awaiting you patiently at the front door.
"Perfect, you found you're way, Mr. Rock."
+ "You're hilarious, Mr. Pigeon."
"Thank you, I try, but with you around it's a lot easier," he jests.
-> Chateau1
+ "There's not many places to go around here."
"Regardless, I'm proud of you for walking."
-> Chateau1

== Chateau1 ==
You side-eye Boris. "What do you mean by that?"
"Whatever you wish it to imply," he says.
"Anyway..." Boris says while fumbling with some keys, "I'll let us in, prepare yourself, I know not what lies on the other end of this door any longer."
Boris finds the correct key and pushes the doorway. The doors creak and whine until full opened. Light rushes into the main hall of the chateau, and, shortly after, you follow suit.
-> ChateauMainHall