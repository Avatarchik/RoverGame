using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Quest : MonoBehaviour
    {
        public List<QuestObjective> objectives = new List<QuestObjective>();

        protected int currentObjective = 0;

        public QuestObjective CurrentObjective
        {
            get { return objectives[currentObjective]; }
        }


        public virtual void Initialize()
        {

        }


        public virtual void CleanUp(QuestObjective questObjective)
        {
            questObjective.CleanUp();
        }


        public virtual void CompleteObjective()
        {
            //TODO run whatever other effects we want on completion
            CleanUp(CurrentObjective);

            currentObjective++;
            BeginObjective();
        }


        public virtual void BeginObjective()
        {
            //TODO run whatever other effects we want on begin
            CurrentObjective.Initialize();
        }
    }
}

