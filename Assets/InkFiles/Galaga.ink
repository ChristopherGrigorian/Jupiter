VAR SpokeToWoman = false
VAR TTBAC = false

= Galagos 
{FirstTimeGalaga:
    You feel yourself being stripped away from reality. 
    You unravel over and over again. You are woven whole. The image of the condos and chateau materialize before you. You are standing right before the quartyard leading to the chateau. 
    This whole place feels earily...Earthly. No harsh wind, no flimsy floor. A bright blue sky rests behind the chateau. You hear birds chirping, the air feels crisp and wonderful.
    ~ FirstTimeGalaga = false
    + _Look around for Boris._
        ->LeftOfFront
    + _Explore on your own._
        ->FrontOfQuartyard
- else:
    This is Galaga.
    ->FrontOfQuartyard
}

== Quartyard ==
The quartyard is very grandiose and breathtaking. Statues of angels and fountains are positioned symmetrically on either side of the walkway heading to the chateau.
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
+ _Leave the quartyard._
    ->FrontOfQuartyard
}

== ANZWAR ==
"Woah woah woah!" shouts Anzhela. "I wouldn't head in there traveler, it's been overrun by rogue inventions! It's far to dangerous."
You heed Anzhela's warning, and don't proceed to the chateau.
->Quartyard
== LeftOfFront ==
You start walking to your left, and see Boris eyeing you from a bench.
+ _Head over to Boris._
    ->TTB
+ _Turn Around_
    ->FrontOfQuartyard
    
== FrontOfQuartyard ==
You are standing in front the quartyard that leads to the chateau. You can see large and immaculately maintained hedges, and canaopy welcoming you into the quartyard.
There is also both a pathway to your left and right that walk along the perimeter of the quartyard.
+ _Enter the quartyard._
    ->Quartyard
+ _Go down the left path._
    ->LeftOfFront
+ _Go down the right path._
    ->RightOfFront
    
== RightOfFront ==
You walk along the perimeter, guided by tall hedges that encapsulate the quartyard. A lone lamp post stands firm in the ground at this corner. A bench rests under it.
+ _Sit on the bench_
- You sit on the bench. It feels uncomfortable. You get up.
+ _Turn the corner_
    ->Condos

== Condos ==
You're met with a small strip of colorful condos. A woman can be seen resting on one of the porches.
+ _Talk to the woman._
    ~ SpokeToWoman = true
    ->TTW
+ _Turn around._
    You turn around and head back around the corner once more. 
    -> FrontOfQuartyard
    
== TTW ==
{SpokeToWoman:
You approach the woman.
~ SpokeToWoman = false
+ "Hello."
-> TTWCONT
- else:
The woman wishes not to speak with you.
->Condos
}

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
The woman no longer wishes to speak to you. You get off of her porch.
->Condos

== AnzhelaShop ==
~ OpenShop("Anzhela's Shop", "Quartyard")
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
Walk with me, Boris gestures. You walk with him towards the front of the quartyard. 
"Anzhela, the merchant in the quartyard, will sell you a vision, or rather data, about specific weapons," Boris says, "I think she has a revolver for sale. If you haven't already, you should really buy that from her to enhance your ability to manifest your revolver."

+ "Understood."
- "Great, I'll leave you here, meet me at the chateau when you're ready."
->FrontOfQuartyard

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