=== OtherNPC ===
{FindSomeOneQuestState: //variable need ended on "State"
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    -else: -> END
}

= canStart
    you can start the find someone quest#speaker:NPC2 #anim:Wave 
    ~StartQuest(FindSomeOneQuestId)
- -> END

= inProgress
This quest is in progress
->END

= canFinish
This quest is can be finished
~FinishQuest(FindSomeOneQuestId)
->END

=finished
The quest is finished you are genius
->END

