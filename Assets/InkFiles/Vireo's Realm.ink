VAR FirstTimeNeonCity = true
== NeonCityHolding ==
A neon city materializes before you as you teleport to the coordinates. You seem to be in a holding area prior to the entrance. A large banner streams in front of you reading "Welcome To Salvation."

{FirstTimeNeonCity:
Boris materializes besides you. Lucy follows suite soon after.
"Hmph..." Boris exhales. 
"Certainly a bold statement as greeting," says Lucy.
"So... is the city's name Salvation?" says Boris.

+ "Probably."
"Yeah... probably..." Boris chirps.
You glance to him, to which he returns a cunning glare.
"Yeah... probably..." you respond.
->NCH1

+ "I don't know, let's ask Vireo."
"You're not very helpful are you?" Boris responds.
->NCH1
}

+ _Enter the city._
    ->NCHub

== NCH1 ==
"Anyway..." Lucy says. "It doesn't matter what the place is called let's just get going."
The party advances towards the city, only to be stopped almost immediately by a toll gate.
There appears to be a single guard standing watch. The guard overlooks a booth.
Boris walks ahead of the party and approaches the booth. "10 pigeon bucks?! What the fuck is that."
The guard looks longingly towards Boris "Art..."
"...what?" asks Boris.
The guards eyes begin to shimmer, "It's art... everything is art here. Vireo is so insipirational... If only I had a fraction of his talent... haaaaah... ohhhhh..."
The guard begins feeling all over their body.
"You're making me very uncomfortable..." says Boris.
Lucy rushes to Boris side and whispers to him, "Boris... just remember none of these people are real... they're Vireo's creations. Whatever you do, try not to provoke anybody."
"Um... okay..." he whispers back.
He looks back towards the guard, "So... um... hate to interrupt whatever is going on here but what is pigeon bucks."
The guard takes a deep breathe and returns their attention to Boris. "It's... a feeling... almost... The concept of currency, pigeons, how intrinsically they are so connected but not at the same time..."
The guard grabs their head in both hands and grovels, "It's just so... so... BRILLIANT."


+ Maybe we can exchange Boris for entry...
VAR ChosePigeon = true
"HEY I'M NOT A PIGEON MAN," Boris shouts.
"Alright let's just calm down..." Lucy says. 
Lucy begins looking around as if looking for something.
->NCH2
+ Maybe there's another way in...
->NCH2

== NCH2 ==
"I'm not sure there is another way in..." Lucy says. "Just look at the surrounding banister, there's nowhere to go besides through the gates. We'll just fall through the realm."
"There's no need for that, friends," the guard says. "Perhaps I could look the other way if you can demonstrate to me a display of art!"

+ "Display of art?"
"DISPLAY THIS BITCH," Boris shouts.
"STOP!" Lucy and you shout in unison.
Boris pulls out his tome and fires a tetra burst at the guard.
The bolt strikes the guard hard, but they barely stagger.
"Ohhh.... HAAAAH... OHHH... THAT... WAS... MARVELOUS..." the guard shrieks in ecstacy. 
"They're a fucking freak," Boris screeches. "Are you both with me or not..."
Lucy and you look to him wide-eyed.
"Please help..." he mumbles.
Prepare for combat.
~ StartCombat("NeonGuard", "NCH3")
->DONE

== NCH3 ==
The guard falls to their knees and tosses their hands to the sky.
"Oh... How beautiful that truly was," the guard begins to cry. "Was this your reckoning Vireo? I'm at the edge of salvation, I've served here all my life, and here I rest... Here... I die..."
The guard dematerializes into a flurry of flower petals.
"I'm getting fucking shivers over here, let's get to the city now..." Boris says.
"Since when have you become such a brute? Get it together that was incredibly risky," Lucy says.
"DON'T TELL ME WHAT TO DO!" yells Boris.
"YOU'RE GOING TO GET US ALL KILLED!" responds Lucy.

{ChosePigeon:
+ "Slow your roll pigeon bro."
"IF YOU SAY THAT ONE MORE TIME!"
"ALRIGHT STOP SHOUTING!" yells Lucy.
-> NCH4
}

+ "CAN WE ALL STOP SHOUTING!"
"YES, FINE, DAMN!" yells Boris.
-> NCH4

== NCH4 ==
Boris takes a few deep breaths, "I feel like this place is making me unecessarily angry. Maybe it's just super bright."

+ "Have you eaten recently?"
"No..." Boris pouts.
"Okay... we are robots you know we don't have to eat things..." says Lucy.
"But I made it a personality trait of mine to be hangry," responds Boris.


+ "..."
"I guess we're going food searching..." says Lucy.
The party walks through the entrance and into the city.
    -> NCHub

+ "Alright, let's go find you some food.
The party walks through the entrance and into the city.
    -> NCHub

== NCHub ==
The city's building and skylines unfold towards the sky as you walk beneath them. Everything--the ground, the building's walls, and the store fronts are lined with neon lines that intertwine and radiate various obnoxious colors. Signs for all sorts of services and shops jut from walls. Up above soaring in the sky are flying trains that seem to be suspended via magnetic lines all throughout the skies.
You are in but a small fragment of this great expanse. Buildings can be seen towering in the distance, all created from a slightly reflective black composite material. No face of any building is flat, and is instead textured by small cubes of this mysterious material jutting at various height from each wall--and even the ground.
The whole city, or at least this portion, feels eerily empty and quiet. 

{FirstTimeNeonCity:
Banners line the street advertising a play--"Only in theaters today! The Tragedy Of Artifice And His Several Angels."
"Do you all see those banners?" asks Lucy. "It's a play by... Vireo?"
A call echoes through the empty street, "Did someone say Vireo?"
The whole party looks in fear towards the noise. 
A man emerges from one of the shop fronts, "Oh... I've never seen you bunch around here before. You must be new here!"
Boris starts coughing as a means for improvisation, "Uh... erm.. *cough*... yes, we are new arrivals. We are just so... so... in love with the architecture here!"
"Oh it's wonderful isn't it," the man trembles. "So inspiring, so dilute. The neon lights bring out the depravity of the Eidolon bricks. Your own reflection in everything... washed in everlasting void. The neon..." he pauses. "Oh... the neon... a smear of color, the lines that divide and wish to cut apart the very fabric that constitutes this world. In vain... always in vain--a pity existence."
Boris opens his mouth as if to say something but the man cuts him off.
"Notice how the lines cross but never join... the meeting between people, brief moments, but never enriching. Each invidual bursting with color but they burst alone... they burst alone...." he cries. "The neon, the people, are meaningless against the grandiose and overpowering consumption of the dark energies that encompass the Eidolon. In the end that connection is meaningless, but the lines never end, each being suspended in continuity. Always forwards... always forwards..." the man dozes.

"Forwards towards... what?" Lucy asks.

"Who knows?" he responds. "It's all just art. At least, that's my interpretation of the great work of Vireo."

Boris leans into you, "Yo... this guy-"
"Cut it out..." you hiss.

"Speaking of Vireo..." says Lucy. "We're actually really big fans, and were wondering where to see his play?"

The man smiles, "Ticket booth is down the street and you take a right. You can't miss it."

"Thanks!" Lucy smiles. "I can't wait!"

Lucy and you are about to walk away from the man, but Boris stands his ground.

"Hey man..." Boris announces to the guy.
Lucy and you both grit your teeth in fear of what he might say.
Boris smiles, "I'm hungry and it seems you got a nice shop here--it's a bakery right?"
"Yes! My pieces are artisanal wonders, they taste amazing, and-"
"Yeah, yeah, do you have muffins?" Boris interrupts.
"...um... yes..." the man responds. "But don't you want something more exciting?"
"No," responds Boris.
"Alright that will be 5 pigeon bucks please," he responds dully.
"Can I repay you in art?" asks Boris.
"Sure!" the man becomes excited again.
"Alright I need the muffin though... as a prop... to show you," responds Boris.
"Oh of course..." the man rushes into the store and returns with one giant gooey choclate muffin and hands it to Boris.
"Alright, watch closely..." Boris says. He proceeds to shove the whole thing in his mouth in one bite. He chews for a solid thirty seconds before finally swallowing.
"See how beautiful that was?" asks Boris.
The man is enamored, "I do... I really do... The glutenous devour, the jab at capitalism, the poor man's performance for but a morself of sustenance, the lack of shame-"
"Alright thats enough," says Boris.
The man now looks at Boris with a tinge of upsetness.
"I mean... you flatter me... really... haha... I'm getting all flustered here!" says Boris.
"Oh..." the man let's out a slight chuckle. "I didn't mean to be so overwhelming. I wonder if Vireo feels the same way..."
In the man's pondering the party is able to slip away.
"Alright I'm sick of these crazy people... but at least I'm fed. Let's head to ticket booth. The guy said it's to the right," commands Boris."

}


+ Turn left down the street.
    {FirstTimeNeonCity:
        "Now's not the time to wander Aiolos... we can explore later, let's get the tickets first."
        You head to the right.
        -> RightStreet
    
    - else:
        ->LeftStreet
    }
+ Turn right down the street.
    -> RightStreet
    

== LeftStreet ==
No story here.
-> DONE

== RightStreet ==
The party wanders down the street. And takes a right at the intersection.

{FirstTimeNeonCity:
"I think I see the ticket booth over there."

-> DONE