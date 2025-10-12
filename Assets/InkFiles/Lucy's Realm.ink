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
->DONE
