
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)
EXTERNAL DialogueAnimationPlay(string animatorId)

VAR MoveToPosQuestId = "MoveToPosQuest"
VAR FindSomeOneQuestId = "FindSomeOneQuest"

VAR MoveToPosQuestState = "CAN_START"
VAR FindSomeOneQuestState = "CAN_START"


/*
VARIABLES:
  anim -> for animation play(Wave, Yell, Disappoint)
  speaker -> hwo will speak(NPC)
 
SPEAKER:
  you need to set speaker every time you make an animation
*/


//Ink-files
INCLUDE npc_quest.ink
INCLUDE second_npc_quest.ink



