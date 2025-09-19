
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
* _Enter the chateau._
    ->ANZWAR
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
    VAR SpokeToWoman = true
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
Nothing here yet.
->DONE