INCLUDE Globals.ink
INCLUDE ProxyWorld.ink
INCLUDE Garden.ink
INCLUDE Government.ink
INCLUDE WholeNewWorld.ink
INCLUDE Galaga.ink
INCLUDE Simulation.ink




The balcony surveys well above the street. The street is empty and quiet. Almos is quiet. He sits across from you now, a table between you both. He usually isn’t quiet on Saturdays. Perhaps you’ve gotten it all wrong. It may be Sunday now that you think about it. The days don’t seem to matter anymore, at least to you, anyway.
~ AddCharacter("Aiolos")
~ AddWeapon("Revolver")
You have a cigarette in hand. Smoke leaves your lips. Almos' face is briefly obstructed from your sight. You're worried he has just disappeared.


 + "Almos?"
 ~ BotherAlmos += 1
    -> TB1
 + "..."
    ->TB1
    

== TB1 ==
{BotherAlmos > 0:
    He doesn’t acknowledge you calling his name. You couldn’t help but feel he was gone in that moment. You remind yourself that the smoke just had to clear a little. 
    Everything is fine. You can see Almos now. 
    Almos is looking out from the balcony down to the street.
}

Almos also has a cigarette in his hand. Two days ago, you poked holes through all of his cigarettes so he wouldn’t be able to smoke them. You’re not sure how either of you is smoking. The cigarettes should have been duds. 
Your mind is a jumbled mess, trying to figure out all the details. 
You ignore the thought for now in defeat.
The balcony is small. You and Almos are sitting upon an aluminum patio set. Just two chairs and a small circular table between you. 
Three months ago, you both barely sat here. It has become something of a ritual for you both to sit here and watch the sunset.
The aluminum of the table seems to be falling apart. You don’t blame the furniture, really, for not holding up. Neither you nor Almos took any particular measures to care for it. It was a cheap set, anyway.

+ _Kick the table leg in frustration._
    ~ UncannyPeculiarity += 1
    ->TB2
+ _Examine the table more closely._
    ->TB2

== TB2 ==

{UncannyPeculiarity > 0:
    You kick the table leg and the whole thing rattles.
    Almos jumps a little, and says nothing while giving you a sharp but brief stare.
    
    You’re becoming angry, but you find it challenging to continue to be upset at metal. Perhaps your outburst has little to do with the table itself, and more to do with frustration of feeling ignored by Almos.
    - else:
    You look closer at the table to see brownish specles forming near the legs. It's not really immediately noticeable, but the table itself feels like it's about to collapse at any second. 
    Only three legs of the four are ever touching the ground at once. You do wonder though if those brown spots you're seeing are actually rust.
}
{JMP1()}

+ _Pull out your phone from your pocket and search for the answer._
    ->TB3
+ _Try to grab Almos' attention again._
    ~ BotherAlmos += 1
    ->TB3
    

== TB3 ==
{PhonePickup == true:
    Almos seems captivated in his thoughts. You shouldn’t bother him when you can just look up the answer. You just like hearing his voice, you guess. You pull out your phone.

    No. Aluminum doesn’t rust, apparently. It corrodes. Interesting, you suppose. You leave the phone out on the table.
}

You try getting Almos’ attention again by touching his hand, but he stays fixated on the street below. He gets enraptured like this sometimes. Perhaps he’s daydreaming. Maybe he isn’t even really here.
His body is before you, but it's as if his mind is on another planet, for all you know. 
You sit in silence for a bit longer.

+ _Daydream along Almos_
    -> Daydream
    
== Daydream ==
Your mind starts to wander admist the silence between you both. 
Without constant stimulation, what are you if not just flesh and meat?
You close your eyes and envision this tableset belonging to a cafe in the middle of a garden.
Oh, you can see it now. You and Almos are at a lovely coffee shop, which also happens to be in a garden. 
You feel upkept, bourgeois even, as a nice breeze sifts through your hair, and a coffee that's consumed your pocket money waits patiently in front of you. 
You romanticize the scene more. You’re on a small bridge, just above a nice running stream of water, which also happens to be where a coffee shop has put some seating–in the middle of a garden, that is. 
You think that would be nice. Almos doesn’t like coffee, though. You suppose he liked feeling tired. You think he’d like it just as much as cigarettes. 

You wonder if the garden would have nice flowers. Although you hope there wouldn’t be any flowers close to where you and Almos would be sitting. You get scared of bees, among other things. 
Speaking of gardens, you’ve never asked Almos what his favorite flowers are. You think he likes carnations. Maybe you should ask him.

+ "Almos?"
    -> Daydream1

== Daydream1 ==
“Hm,” he mutters.
The street is still holding his gaze. His side profile is the only thing greeting you. He looks frozen. His forearm is like a pillar managing the integrity of his skull. His cheek and chin rest nicely in his hand, serving nicely as a capital. 
You don’t know if you’re bothered by the fact that he’s not looking at you. His eyes look sullen and droopy. You think he may be tired. He’s always tired, though. You’ve already forgotten what you were going to ask him. 
An uncomfortable amount of time seems to have passed, marked by his pupils slowly orbiting over to you.
“What is your favorite flower?” you finally blurt as if your heart had jump-started your lungs.

He still doesn’t respond. His other hand is still occupied, placing a cigarette in his mouth. You feel like you’ve tried forever to get Almos to stop smoking, but he kind of ropes you in as well. 
You don’t reckon you have much of a head over your shoulders. 

You bought him gum a while back as a substitute, but you think he likes both gum and cigarettes now. He alternates. You’ve tried hiding his cigarettes, but Almos is really smart. He can always find where you’ve hidden them. Sometimes you’ve thought yourself as doing an incredible job. 

Smoke shrouds Almos once again. For those few seconds, you feel like he’s vanished. He returns to you with the cigarette in his mouth again. He inhales and sighs cotton wisps. 

Images of you poking holes through all his cigarettes resurface within your mind once more. You just realized, how are you both smoking? Did you miss a pack or something? You examine your cigarette, sure enough, no holes. 
You can't quite pin your emotions, but you feel yourself becoming a very potent mixture of anger and sadness. You feel like purple. You feel like if lavender didn’t have a smell.
You direct your attention back to Almos.

+ "Almos, how are we smoking?"
    -> TB5
+ "How do you even afford new cigarettes?"
    -> TB5
    
== TB5 ==
“Orchids.” He interjects quietly while slowly turning towards you. His face grew heavy with shame as he dropped his gaze. 

+ "What the hell are you talking about?"
    -> TB6
+ "Stop trying to distract me! Just answer the question Almos!"
    -> TB6
    
== TB6 ==
Almos remains silent.
You're about to blow a fuse.
He relinquishes his cigarette in the ashtray between you two. He once more stares back at the empty street down below.
Why were you so easily provoked at nothing? You hope you didn't make Almos feel sad. You feel the need to say something, but you're a little choked up at the moment.
You try grabbing his hand to comfort him, but he quickly retracted it to his lap. You give out a heavy sigh in response.

+ _Look out into the street with Almos_
    -> TB7
    
== TB7 ==
Can't beat 'em join 'em, you suppose. You join Almos in looking out in the empty street.
As the sun begins to set Almos resigns from looking out. You feel him staring at you now, but you don't meet his gaze. You stare on as he once was, not out of choice, but a feeling of necessity. 
Something beyond starts to demand your attention. You find siren in the moon's image. 
Were these the same forces acting upon Almos? He seemed so different today. Inexplicablly compelled to the horizon.

The moon shimmeres more dramtically than before, as if it were purposely dazzling you. 
Light waves waft through the skies. They call to you. 
An irresistible sensation akin to a rush of adrenaline overwhelms you. You feel compelled to melt away with the sky.  

Almos’ attention is now entirely on you. It seems your and Almos’ demeanor have completely switched. 

You get out of your seat to lean against the banister. You stand high above the charcoal-black ground. The white stripes of the road reminded you of stars. 
Leaning over the banister isn't enough. You must greet the sky. Become one with the stars and nothingness between.
You maneuver yourself over the banister, one leg after the other. 
You're in a moment of mania. You wish to feed into the euphoria, and get closer to the stars.
There is just enough space on the other end to hold your heels. You know not if you're shaking in fear or excitement. 
Your eyes glisten with the moon’s image, filling your body with envy. For a moment, you wish you could shimmer in Almos' mind the very same way the moon has enthralled yours.
You lull and drift off into the moon’s ocean of light. 
Almos’ eyes calmly chase you seven stories down into the street.

+ _Continue..._
    -> EndOfAnEnd

== EndOfAnEnd ==
~ FadeOutSeq("Jupiter is an empty street. / Jupiter is  relentless grief. / Jupiter is the tabacco between your teeth.", "Rabbit")
->DONE

=== function JMP1 ===
“Almos,” you’re not even sure you consciously expelled his name from your mouth. Can aluminum rust?”

You always seem to just say his name when you have a question. You think Almos is really smart. He knows a lot of things.

He doesn’t respond to you, however.
        
