=== MoveToPos ===
{MoveToPosQuestState: //variable need ended on "State"
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    -else: -> END
}
    
= canStart
You find a desiccated figure wrapped in tattered robes. Their whisper is barely audible over the howling wind.#speaker:NPC #anim:Wave 

Ash... you have come... Listen...
Here, where stones remember the fall of gods, I await one who dares seek Truth among the shadows.#anim: Yell
*[Touch the memory]What do you know?
    ~StartQuest(MoveToPosQuestId)
*[Retreat]This place reeks of ruin
- -> END

= inProgress
The figure remains motionless, but its voice echoes directly in your mind.#speaker:NPC #anim: Disappoint

You still walk the path... I see glimmers of steel in the fog.
Return when ten altars of silence have borne your step.
->END

= canFinish
You emit a faint glow. The creature raises its head.

Ten times you have stood upon the edge... and ten times returned.
The dust of ages settles differently upon you. Your gaze... your gaze has changed.
Very well, accept that for which you have walked.
~FinishQuest(MoveToPosQuestId)
->END
*[Receive the revelation]
->END

=finished
The being begins to crumble before your eyes, its purpose fulfilled.

Revelation... is not a gift, but a burden. Now you see through the veil of this world.
Go forth... and remember the silence between steps.
->END

