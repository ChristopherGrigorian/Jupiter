INCLUDE Globals.ink
INCLUDE ProxyWorld.ink
INCLUDE Garden.ink
INCLUDE Government.ink
INCLUDE WholeNewWorld.ink
INCLUDE Galaga.ink
INCLUDE Simulation.ink





The balcony surveys well above the street. The street is empty and quiet. Almos is quiet. He sits across from you now. He usually isn’t quiet on Saturdays. Perhaps you’ve gotten it all wrong. It may be Sunday now that you think about it. The days don’t seem to matter anymore, at least to you, anyway. The radio was playing gurbled nonsense so you just shut it off. The damn thing must have been broken. 

You have a cigarette in hand. Smoke leaves your lips. Almos' face is briefly obstructed from your sight. You're worried he has just disappeared.


 + "Almos?"
    -> TB1
 + "..."
    ->TB1
 + "Follow Rabbit"
    ->FollowRabbit
+ T1
    ->T1
    

== TB1 ==
{BotherAlmos > 0:
    He doesn’t acknowledge you calling his name. You couldn’t help but feel he was gone in that moment. You remind yourself that the smoke just had to clear a little. Everything is fine. You can see Almos now. Almos is looking out from the balcony down to the street.
}

Almos also has a cigarette in his hand. You remember that two days ago, you poked holes through all of his cigarettes so he wouldn’t be able to smoke them. You’re not sure how either of you is smoking. The cigarettes should have been duds. You ignore the thought for now.

The balcony is small. You and Almos are sitting upon an aluminum patio set. Just two chairs and a small circular table between you. Three months ago, you both barely sat here. It has become something of a ritual for you both to sit here and watch the sunset.

The aluminum of the table seems to be rusting. You don’t blame the furniture for rusting. Neither you nor Almos took any particular measures to care for it. 
+ _Cheap or not, though, you'd wished the patio set had held up much better._
    ~ UncannyPeculiarity += 1
    ->TB2
+ _Can aluminum even rust?_
    ->TB2

== TB2 ==

{UncannyPeculiarity > 0:
    You’re becoming angry, but you find it challenging to continue to be upset at metal with much more ferocity than a measly lighter flame. It’s probably inanimate, but in the slight chance it does hold sentience, the patio set itself may have known it would rust. Hence, this slight chance of being sentient justifies the small amount of anger you harbour against this patio set.
}
{JMP1()}

+ _Pull out your phone from your pocket and search for the answer._
    ->TB3
+ _Try to grab Almos' attention again._
    ~ BotherAlmos += 1
    ->TB3
    

== TB3 ==
{PhonePickup == true:
    Almos seems captivated in his thoughts. You shouldn’t bother him when you can just look up the answer. You just like hearing his voice, you guess. You concede to your thoughts and pull out your phone.

    No. Aluminum doesn’t rust, apparently. It corrodes. Huh. Interesting, you suppose. You leave the phone out on the table.
}

You try getting Almos’ attention again by touching his hand, but he stays fixated on the street below. He gets enraptured like this sometimes. Perhaps he’s daydreaming. Maybe he isn’t even really here. His body is before you, but his mind is on another planet, for all you know. You sit in silence for a bit longer.

+ _Daydream along Almos_
    -> Daydream
    
== Daydream ==
Your mind starts to wander.  The furniture looks like it would belong to a cafe in the middle of a garden. Oh, you can see it now. You and Almos are at a lovely coffee shop, which also happens to be in a garden. You’re sitting at a table just like this one. You’re on a small bridge, just above a nice running stream of water, which also happens to be where a coffee shop has put some seating–in the middle of a garden, that is. You think that would be nice. Almos doesn’t like coffee, though. You suppose he liked feeling tired. You think he’d like it just as much as cigarettes. 

You wonder if the garden would have nice flowers. Although you hope there wouldn’t be any flowers close to where you and Almos would be sitting. You get scared of bees, among other things. Speaking of gardens, you’ve never asked Almos what his favorite flowers are. You think he likes carnations. Maybe you should ask him.

+ "Almos?"
    -> Daydream1

== Daydream1 ==
“Hm,” he mutters.
The street is still holding his gaze. His side profile is the only thing greeting you. He looks frozen. His forearm is like a pillar managing the integrity of his skull. His cheek and chin rest nicely in his hand, serving nicely as a capital. You don’t know if you’re bothered by the fact that he’s not looking at you. His eyes look sullen and droopy. You think he may be tired. He’s always tired, though. You’ve already forgotten what you were going to ask him. An uncomfortable amount of time seems to have passed, marked by his pupils slowly orbiting over to you.
“What is your favorite flower?” you finally blurt as if your heart had jump-started your lungs.

He still doesn’t respond. His other hand is still occupied, placing a cigarette in his mouth. You feel like you’ve tried forever to get Almos to stop smoking, but he kind of ropes you in as well. You don’t reckon you have much of a head over your shoulders. 

You bought him gum a while back as a substitute, but you think he likes both gum and cigarettes now. He alternates. You’ve tried hiding his cigarettes, but Almos is really smart. He can always find where you’ve hidden them. Sometimes you’ve thought yourself as doing an incredible job. You were positive he wouldn’t find them this most recent time you’ve hidden them, until of course the moment he actually did. Honestly, if you’d swallowed them, you’re sure he’d still find it–intact at that rate.

Smoke shrouds Almos once again. For those few seconds, you feel like he’s vanished. He returns to me with the cigarette in his mouth again. He inhales and sighs cotton wisps. 

You poked holes through all of his cigarettes two days ago. You just realized. How are you both smoking, you think to yourself? Did you miss a pack or something? The cigarettes you’re holding are intact. There aren’t any holes to prevent them from being smoked. How does he still have one, let alone you? You feel yourself becoming a very potent mixture of anger and sadness. You feel like purple. You feel like if lavender didn’t have a smell.

+ "Almos, I feel like I've told you a million times to stop smoking. Isn't this strange? I'm not sure how this keeps happening."
    -> TB5
+ "How do you even afford new cigarettes? I thought we blew through our allowance already?"
    -> TB5
    
== TB5 ==
“Orchids.” He interjects quietly while slowly turning towards me. His face grew heavy with shame as he dropped his gaze. 

+ "What the hell are you talking about?"
    -> TB6
+ "Stop trying to distract me! Just answer the question Almos!"
    -> TB6
    
== TB6 ==
The sternness in your voice maintained vigor as the words rose from your lungs. The speech engulfed your throat as quickly as your emotions had swelled. You feel like you are suffocating. Why were you so easily provoked at nothing? You hope you didn’t make Almos feel sad. You feel the need to say something, but you're all choked up with yourself.

Almos relinquishes his cigarette during your half-ass apology in the ashtray between you two.

You try grabbing his hand to comfort him, but he quickly retracted it to his lap. Soon after, he resigned, staring back at the empty street.

+ _Look out into the street with Almos_
    -> TB7
    
== TB7 ==
The sun slowly began to set. Almos slowly began to resign from guarding both the sky and the street. You stare on, though. Something starts to captivate your attention. Were these the same forces acting upon Almos? He seemed so different today. Inexplicable compelled to the horizon.

The moon shimmered more dramatically as you fixed your eyes on the sky. Light waves wafted through the skies. They called to you—irresistible and sweet. You felt compelled to melt away with the sky. 

Almos’ attention is now entirely on you. It seems your and Almos’ demeanor have completely switched. 

The sun has completely vanished from the sky. Almos felt a new sensation become of him. He felt ethereal. He felt happy. Of course, the tarry skies did not influence the both of you in the same manner. Almos saw change. Reciprocally, you saw an end.

+ _Continue..._
    -> TB8

== TB8 ==
You got out of your seat to lean against the banister. The balcony was high above the charcoal-black ground. The white stripes of the road reminded you of stars. You maneuvered yourself over the banister. There was just enough space on the other end to hold your heels. Your eyes glisten with the moon’s image, filling your body with envy. You wish you could shimmer in Almos’ mind the very same way. With defeat in your heart, you lull and drift off into the moon’s ocean of light. Almos’ eyes calmly chase you seven stories down into the street.

+ _Continue..._
    -> EndOfAnEnd

== EndOfAnEnd ==
~ FadeOutSeq("Jupiter is an empty street. / Jupiter is  relentless grief. / Jupiter is the tabacco between your teeth.", "Rabbit")
->DONE

=== function JMP1 ===
“Almos,” you’re not even sure you consciously expelled his name from your mouth. Can aluminum rust?”

You always seem to just say his name when you have a question. You think Almos is really smart. He knows a lot of things.

He doesn’t respond to you, however.
        
