using UnityEngine;
namespace Trigger
{
    /*
     * \brief Абстрактный класс для написания\наследования Action
     * 		  Action служит, чтобы выполнить некоторую работу
     *    				  
                              Trigger
                                 |
                                 |
                       +---------+---------+
                       |                   |
                       |       Run         |
                       |                   |
                       +---------+---------+
                                 |
                                 |
                       +---------+---------+
                       |                   |
                       |   Update(if need) |
                       |                   |
                       +---------+---------+
                                 |
                                 |
                       +---------+---------+
                       |                   |
                       |       Over        |
                       |                   |
                       +-------------------+

     * */
    abstract public class Action : MonoBehaviour,
                                    IDebug,
                                    ITriggerStates,
                                    ITriggerExecute
    {

        ///< Выводить отладечную информацию
        public bool debug = false;

        ETriggerStates state = ETriggerStates.NOT_ACTIVATED;

        abstract public void Run(GameObject obj);

        public void Log(string msg)
        {
            if (debug) Debug.Log(name + ".action: " + msg);
        }

        virtual public void Pause()
        {
            Log("base pause detected");
            //enabled = false;
            SetState(ETriggerStates.PAUSE);
        }

        virtual public void Continue(GameObject obj)
        {
            //enabled = true;
            SetState(ETriggerStates.RUNNING);
        }

        public void SetState(ETriggerStates state)
        {
            this.state = state;
            enabled = state == ETriggerStates.RUNNING;
        }

        public bool IsState(ETriggerStates state)
        {
            return this.state == state;
        }

        public abstract void Update();
        public abstract void End();
    }
}