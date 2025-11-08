VAR WhichName = 0
== DressingRoom ==
The party makes there way through another set of back rooms before reaching a door boldly labeled with "Serzh."
"So... you've scammed people before," says Boris.
"I procure talent... and what of it?" Serzh hisses.
"Nothing... just checking..." Boris smirks.
Serzh send Boris an aggressive glance, he squints his eyes, "Don't bite the hand that feed you, okay? You want in to Vireo play don't you?"
"Just... address me from now on," Lucy interjects. "Are friend has gotten a little bit of a tantrum lately..." Lucy glares to Boris now, "I suppose it's because a certain important someone called him out on his love?"

"NO!" Boris shouts.

Lucy prompts once more "Well let's address it now-" 

"NO!" Boris shouts, "I said no, I mean no."

Lucy crosses her arms as if in pout, "Fine, I'll let it go for now-"
"You'll let it go forever," it seems like Boris is on the verge of tears.

+ _Hug Boris._
You try to console Boris, but he just pushes you away.
->DR1
+ _Ignore Boris._
->DR1

== DR1 ==
"So... strategy?" you ask Serzh. "Who are we up against."
"First opponent should be a breeze," Serzh affirms... "But... that's not the immediate problem. You all are newcomers. You have to win the audience's favor, or else nobody is going to bid on your success."
"YOU'RE GAMBLING ON US AS WELL?!" Boris shouts.
"Not me... not allowed," he laughs. "Some friends of mine."
Boris gives Serzh a dissaproving glare.
"Anyway... you all need a cool name... and maybe some thing to spruce up your outfits. What kind of name are ya'll thinking for your group?"

+ "Galactic Meltdown."
"Alright... Galatic Meltdown it is, sounds rad, man."
~ WhichName = 1
    ->DR2
+ "Muffin Smashers."
~ WhichName = 2
Serzh looks to you in confusion, "Um... are you sure?" 
"YES" screams Boris. "YES."
"Al- Alright then. Muffin Smashers it is."
    -> DR2
    
== DR2 ==
"Now for costumes..." Serzh says.
"I don't think that'll be necessary," says Lucy. "Take a look."
Lucy let's Andromeda's energy overwhelm her. She burns a brilliant rose-gold color as her entire appearances becomes android like. She is very resemblant of Andromeda, her outfit is that of a galatic guardian.
Lucy's voice can be heard overlaying with Andromeda's, "Cool look, huh?"

Serzh looks almost distrubed, "I'm incredibly intrigued... oh... the crowd is going to love this!"
He looks wildly around the room, "Can all of you do that?"
"Sore subject..." Lucy giggles.
Serzh looks over to you and Boris.
Boris is the only one of you two that looks like he's about to cry.

+ "I can as well."
    -> DR3

+ "I'm not sure I could do it on demand."
"It's all just confidence Aiolos," Lucy beams. "He can do it too Serzh, trust me."
    -> DR3

== DR3 ==
"Alright then," Serzh snuffs. "Your first match is against Celestial Opposition. They're a group that's skilled in ranged magic and arts. Think you can handle that?" 

+ "Absolutely."
"When's the match?" Boris droops.
-> DR4

+ "I'm not too sure."
"Don't... listen to him... we got this," Boris droops. "When's the match?"
-> DR4

== DR4 ==
Serzh smirks, "Don't you hear the crowd roaring out there? It starts soon, champs."
The party stands near the door but only light mumbles can be heard--
"WELCOME---AS YOU ALL KNOW--MY NAME IS PIVIN--ARE YOU READY--ART UNFOLD."
A sea of chanting rumbles the very walls of the room your in.
"I'll bring you to the cage," Serzh says.
Serzh leads the party through the halls once more towards the arena room. The crowd roars as soon as they catch sight of you in the party. Pivin gets on the microphone, "INNN THE ARENA, WE HAVE OUR VERY OWN DIVINE TRILLLOOOGGGYYY--CELESTIAL OPPOSITION. ANNNDDD COMING IN HOT IS..."
{WhichName == 1:
"GALAAACTIC MELTDOOOWWWNNN. CAN THESE NEWCOMERS RESIST THE ULTIMATE OPPOSITION?" Pivin roars.
- else:
"Muffin Smashers? *ahem* I MEAN MUFFIN SMASHERRRS. SOFT BUT DEADLY, HUH? CAN THESE NEWCOMERS RESIST THE ULTIMATE OPPOSITION" Pivin roars.
}

Serzh lets the party into the cage, "Good luck you all, I'm counting on you," he says in farewell as he locks you within the cage.
Boris cracks his knuckles before whipping out his tome. "Alrighty I'm ready."
He looks to you and Lucy as if prompting you both to get ready. Lucy manifests her weapon.
Prepare for combat.
~ StartCombat("Celestial Opposition", "DR5")
-> DONE

== DR5 ==
You hear Serzh shouting hysterically for you. He's fired up, and so is the party.
The crowd roars for you. "THAT...WAS...INCREDIBLE... THESE NEWCOMERS TORCHED THE OPPOSITION, BUT CAN THEY HANDLE THE HEAT?" screams Pivin. "NEXT UP IN THE RING IS KAELIX AND HIS CONSTRUCTS."

"YOU ALL GOT THIS," Serzh shouts throught he crowd's cheering. "THIS GUY IS FIERCE JUST WATCH OUT. HIS CONSTRUCTS ENABLE HIM TO BE STRONGER, TAKE THOSE OUT BEFORE DEALING WITH HIM."

Kaelix leaps into the ring, a magnificent cape flowing begind him. He smirks to the party, his teeth are sharp and jagged. "READY TO DIE?!" he snarls to the party. His eyes are wild. He acts as though he exists beyond his flesh.
"FAT CHANCE!" Boris shouts back.
"Careful Boris..." Lucy says. "I think this guy is on some sort of enhancements..."
"DRUGS?" Boris shouts.
Kaelix just laughs and takes in a deep breathe. The party witnesses Kaelix body spread thin as if smeared himself across a canvas. He becomes a blurred fury of color before three identical Kaelix materialize from the mist.
Each version looks to the party. Each run their hands through their hair of flames as if in taunt, "PREPARE FOR COMBAT!" they hiss simultaneously.
~ StartCombat("Kaelix", "DR6")
-> DONE

== DR6 ==
"AHHH THAT WAS SPICY...NOT! AHAHA!" Boris shouts.
A camera attached to a drone zooms enters the ring and focuses in on Boris. He sticks his tongue out and points to eat while shaking his head. The crowd loves it. "BOR-IS! BOR-IS! BOR-IS!" they chant.
Boris' image is being mirrored on jumbotron after jumbotron high above in the crowd. The party looks up at the rows and rows of onlookers suspended high above the arena. 
Boris is fired up. His spirit is infectious for you and Lucy.
"HEY! HEY! DON'T GET TOO WORKED UP!" you hear Serzh shouting from the sidelines. "NEXT FIGHT IS NO JOKE YA'LL FOCUS UP."
"SHE NEEDS NO INTRODUCTION YALL, THE PRINCE OF DESTRUCTION--SYRION."
A burst of purple fog manifests at the other end of the arena. Manic laughter can be heard permeating from every edge of the arena. "Oh... a crew... a pity, there's only one of me to entertain you all."
Out from the mist emerges Syrion. She looks absolutely weightless, her clothing drapes effortlessly over her as if she were a ghost. Her eyes glisten an opaque lavender. She glides effortless to the center of the arena and pulls out a paintbrush.
"GORGEOUS AS EVER--SYRION YOU CHARMER! WATCH OUT--HER WORK IS BEAUTIFUL BUT DEADLY... CAN THE NEWCOMERS SURVIVE HER EPHEMERAL MAGICS?" Pivin shouts over the speaker.
You turn to Serzh, realizing he's trying to tell you something. The crowd is far too loud-
"Face me!" Syrion commands. Her voice drops and becomes calm and snake-like, "I shall paint you a better reality, one where you will succumb to my pen. I will be your undone."
The ground begins to shift beneath you. The arena erupts in a series of dimensional tears.
Pivin comes on the loud speaker, "THE PARTY IS BEING TAKEN TO SYRION'S DOMAIN--OH... HER BEAUTIFUL YET WICKED TRICKERY... WE'LL SEND IN A CAMERA TO FOLLOW THE ACTION! GOOD LU-"
Pivin's voice is swallowed away. The party is enclosed in a whole different dimension. Syrion stands ethereal before you all.
Boris looks terrified, "Where the hell are we!?" He's absolutely frozen with fear.
"I don't know," affirms Lucy. "Don't be scared, we can do this!"
Syrion just smiles, "Oh you miserable bunch, I'll take away your pain, I'll paint you a beautiful life."
Syrion begins waving her paintbrush about.
"NO TIME TO WASTE!" shouts Lucy. "IT'S TIME TO ENGAGE. PREPARE FOR COMBAT!"
~ StartCombat("Syrion", "DR7")
-> DONE

== DR7 ==
The alternate dimension collapses and the party returns back the arena. The crowd awaits Boris' victory taunt. A camera gets close to his face. Boris is panting heavily, "Ahem... Huugh," he coughs, "Not gonna lie that was kind of hard ya'll like no joke..." The crowd is silent. Boris looks around at the quiet crowd up above. 
"I think I need a smoke break," he smirks. He materializes a cigarette, takes a hit and blows out purple smoke before quickly waving the cloud away. He points the camera, cigarette between his fingers. Boris puffs his chest, "Nah... THIS BRAND IS TRASH!" he shouts pompously before tossing the cigarette to the side.
The crowd goes hysterical. The arena roars louder than they've ever done before.
"PUT THAT HANDSOME MUG ON A T-SHIRT! AHAHAHA!" shouts Pivin.
Lucy and you share a glance and just smile at one another.

"WHAT AN ENTHUSIASTIC AUDIENCE WE HAVE TONIGHT! LET'S KEEP THAT ENERGY UP AND FIERCE YA'LL! WE HAVE ONE MORE MATCH FOR ALL YOU LOVELY FOLK TONIGHT. OUR DEADLY TRIO WILL FACE...DRUM ROLL PLEASE... ... ...RABBIT!"

"Rabbit?" the party chirps in unison. 
The party watches Rabbit walk casually up to the gate to the cage and let themself in. They look upon the party, "Oh good, you all had the same idea as me."
The party looks upon Rabbit in utter shock.
"Come on now, let's get this over with..." Rabbit mumbles. "The show is to begin soon, we need to get the cash and get out."
"GET READY, GET SET, FIGHT!" Pivin shouts.
The party doesn't move a muscle.
Rabbit manifests their gun in hand and walks to the center of the arena. They pull the gun to their own head. "See you at the show..." They pull the trigger and disappears into a constellation of stars.
The crowd is silent.
The party is silent.
"A... FORFEIT?!?" Pivin screams in disbelief.
{WhichName == 1:
"OH..." she says, "I SEE... JUST THE OVERWHELMING THOUGHT OF SUCCUMBING TO OUR GALACTIC MELTDOWN HAS LEFT OPPONENTS BACK PEDALLING! A VICTORY I SAY! AND A GRAND ONE AT THAT!"
- else:
"OH..." she says, "I SEE... JUST THE OVERWHELMING THOUGHT OF SUCCUMBING TO OUR MUFFIN SMASHERS HAS LEFT OPPONENTS BACK PEDALLING! A VICTORY I SAY! AND A GRAND ONE AT THAT!"
}

The crowd goes bat-shit crazy. 
"A BEAUTIFUL DISPLAY OF ART!" Pivin weaps over the microphone. "PLEASE JOIN US NEXT TIME AT OUR NEXT EXHIBIT! UPCOMING IN A WEEK!"
The droves of people begin filing out of the arena theater. Serzh enter to ring and comes to congradulate you all.
"Well... I knew you all could do it," his lip and nose begin to shiver as he weeps, "Thank you for making me a rich man today..."
"Yeah, yeah spare the act, send us the pigeon bucks," commands Lucy.
"Ooh, attidude," chirps Boris.
"And you better give us a little extra," Lucy demands, "We need one more ticket for our Rabbit friend."
"Ahaha... I don't know if I'm feeling that generous..." he smiles.
Both you and Boris draw your weapons pointed straight at Serzh. Your gun is practically in his skull.
"You'know what... I am feeling generous today... 750,000 pigeon bucks to you, and I keep the rest."
Both you and Boris keep your weapons drawn.
Lucy looks to both you, "Put the damn weapons down! We're taken that and going we don't have much time we may miss the play."

The party receives their payout from Serzh and exits the underground arena, resurfacing near the ticket booths. Rabbit awaits you by the vendor. You purchase the tickets for the group. 
"Alright follow me," announces Rabbit, "There's a train station on the left side of that intersection."
The party follows them to the train station.
"It may be a good idea to come back here and look at the information stand for pamplets of the surroundings of the city," Rabbit informs. "For now we don't have time, though."
A train pulls up to the station.
"The theater has an exclusive line heading straight to the heart of it," Rabbit mumbles. "We get off at the last stop."
"Damn, you really did your research, huh?" says Boris.
"Field scouting? Yes," they affirm.
The party rides in silence and get off at the last stop.
You surface from the train station into the heart of the theater. The place is covered in gold and green accents. Marble floors shine radiantly upon you. Rows of red velvet seats await you beyond dark wooden double doors, guarded by ticket checkers.
"Can we go to the concession stand?" Boris begs.
"We're not here to actually enjoy the show..." Rabbit says. "We're hoping Vireo can't help but make an appearance for attention..."
"That's when we strike..." nods Lucy.
"Okay..." Boris bites his cheeks in anger. "I'm still going to go the concession stand."
The whole party waits in the concesison stand with Boris.
# Put a shop here.
"Okay I'm ready..." Boris says as he cradles a bunch of junk food in both his arms.
The party displays their tickets and move into the theater room.
"Where should we sit?" asks Lucy.
"Somewhere in the middle, perhaps, or closer to the back?" Rabbit suggests, "Don't want to draw too much attention..."
"Okay, here looks good then," Lucy says as she ushers the party to seats around the middle of the theater hall.
The party once more sits in silence besides each other--besides the occasional munching or smacking noises from Boris.
You all wait for the show to start. 
-> TheGrandPlay

-> DONE