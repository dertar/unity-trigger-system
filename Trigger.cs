using UnityEngine;
namespace Trigger
{
    /*
     * \brief Абстрактный класс для написания\наследования
     * 		  триггеров.
     * 		  Триггер складается из события и очереди Action.
     * 		  События - когда или как запустится команда цепочек из Action.
     * 				   +-------------------+
                       |                   |
                       |       Run         |
                       |                   |
                       +-------------------+
                                /\
                               /  \
                              /    \
                             /      \
        +-------------------+        +-------------------+
        |                   |        |                   |
        |    Run Syn        |        |      Run Asyn     |
        |                   |        |                   |
        +---------+---------+        +---------+---------+
                  |                            |
                  |                            |
                  |                            |
        +---------+---------+        +---------+---------+
        |                   |        |                   |
        |    Update Syn     |        |   Update Asyn     |
        |                   |        |                   |
        +-------------------+        +-------------------+
                             \      /
                              \    /
                               \  /
                                \/
                       +-------------------+
                       |                   |
                       |      Turn Off     |
                       |                   |
                       +-------------------+
    */


    abstract public class Event : MonoBehaviour, IDebug, ITriggerExecute
    {

        ///< Очередь из Action
        [Header("Последовательность Action")]
        [Tooltip("Очередь из скриптов Action")]
        public QueueActions queue;

        ///< Тип запуска Action скриптов
        [Tooltip("Как будет запускаться последовательность скриптов")]
        [SerializeField]
        protected ETriggerRunMode runMode = ETriggerRunMode.SYNCHRONOUSLY;
        [SerializeField]
        protected ETriggerWorkMode workMode = ETriggerWorkMode.FIRE;

        ///< Триггер будет активироваться один или много раз
        [Tooltip("Скрипт будет запускаться одиножды?")]
        public bool once = true;

        ///< Выводить отладечную информацию
        public bool debug = false;

        ///< Подсчет количество запусков скрипта
        [Header("Кол-во запусков")]
        [SerializeField]
        protected int countTriggered;


        ///< Кто(GameObject) активировал данный триггер.
        protected GameObject whoTriggered;

        ///< Данный скрипт мониторится в Update?
        private ETriggerStates state = ETriggerStates.NOT_ACTIVATED;

        void Start()
        {
            Init();
        }

        abstract public void Update();

        /*
         *	\brief  Апдейт метод для синхронного выполнения 
         *			скрипта
         *	\return bool 
         */
        protected bool UpdateSyn()
        {

            if (queue.Current() == null)
            {
                Log("No action left");

                if (!once)
                {
                    queue.Recharge();
                }
                whoTriggered = null;
                SetState(ETriggerStates.FINISHED);
                return false;
            }
            else if (queue.Current().IsState(ETriggerStates.FINISHED))
            {
                Log("Current action is over, poping next action");
                PopAction(whoTriggered);
                return true;
            }

            return CheckOver();
        }

        /*
         *	\brief  Апдейт метод для асинхронного выполнения 
         *			скрипта
         */
        protected bool UpdateAsyn()
        {
            return CheckOver();
        }

        /*
         *	\brief  Проверяет все ли Action исполнились,
         *			если все исполнились, скрипт отключается:
         *				running = enabled = false;
         *	\return bool
         */
        private bool CheckOver()
        {
            if (IsAllActionsExecuted())
            {
                Log("All actions done!");
                SetState(ETriggerStates.FINISHED);

                return true;
            }
            return false;
        }

        /*
         *	\brief  Метод запускает цепочку из Action.
         *			При запуске Триггер активируется:
         *				enabled = running = true;
         *
         *  \param[in] GameObject - регистрация, кто активировал триггер
         */
        public void Run(GameObject obj)
        {
            if (IsState(ETriggerStates.NOT_ACTIVATED) || IsState(ETriggerStates.FINISHED))
            {
                Log("Starting trigger");
                NewStart(obj);
            }
            else if (IsState(ETriggerStates.PAUSE))
            {
                Log("Continuing trigger");
                Continue(obj);
            }
        }

        protected bool IsExecuting()
        {

            if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
            {
                return queue.Current() != null &&
                    queue.Current().IsState(ETriggerStates.RUNNING);
            }

            return IsAllActionsExecuted();
        }

        public void Continue(GameObject obj)
        {
            Log("Continue");
            SetState(ETriggerStates.RUNNING);

            if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
            {
                foreach (var action in queue.GetActions())
                {
                    action.Continue(obj);
                }
            }
            else
            {

                queue.Current().Continue(obj);
            }
            this.whoTriggered = obj;
        }

        public void Pause()
        {
            Log("Pause");
            SetState(ETriggerStates.PAUSE);
            if (IsState(ETriggerRunMode.ASYNCHRONOUSLY))
            {
                foreach (var action in queue.GetActions())
                {
                    action.Pause();
                }
            }
            else
            {

                if (queue.Current() != null)
                    queue.Current().Pause();
            }
        }

        private void NewStart(GameObject obj)
        {
            if (once && countTriggered >= 1)
            {
                return;
            }
            this.whoTriggered = obj;

            if (IsState(ETriggerRunMode.SYNCHRONOUSLY))
            {
                RunSynchronously(obj);
            }
            else
            {
                RunAsynchronously(obj);
            }

            SetState(ETriggerStates.RUNNING);
            countTriggered++;
        }

        /*
         *	\brief  Выталкает следующий Action из очереди и 
         *			ставит на выполнение
         *
         *  \param[in] GameObject - регистрация, кто активировал триггер
         */
        protected void PopAction(GameObject obj)
        {
            queue.Next();
            if (queue.Current() != null)
            {
                if (whoTriggered != null)
                {
                    whoTriggered = obj;
                }

                Log("Found next action");
                queue.Current().Run(whoTriggered);
            }
        }

        /*
         *	\brief  Cинхронный запуск цепочки Action скриптов
         */
        protected void RunSynchronously(GameObject obj)
        {
            PopAction(obj);
        }

        /*
         *	\brief  Асинхронный запуск цепочки Action скриптов
         *
         *  \param[in] GameObject - регистрация, кто активировал триггер
         */
        protected void RunAsynchronously(GameObject obj)
        {
            whoTriggered = obj;
            Log("Starting asyn actions");
            foreach (var action in queue.GetActions())
            {
                action.Run(obj);
            }
        }

        /*
         *	\brief  Проверяет в очереди все ли Action
         *          закончили свою работу.
         * 	\return bool
         */
        protected bool IsAllActionsExecuted()
        {
            return queue.IsAllActionsExecuted();
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

        public void SetState(ETriggerRunMode runMode)
        {
            this.runMode = runMode;
        }

        public bool IsState(ETriggerRunMode runMode)
        {
            return this.runMode == runMode;
        }

        public void SetState(ETriggerWorkMode workMode)
        {
            this.workMode = workMode;
        }

        public bool IsState(ETriggerWorkMode workMode)
        {
            return this.workMode == workMode;
        }

        public void Log(string msg)
        {
            if (debug) Debug.Log(name + ".trigger: " + msg);
        }

        /*
         *	\brief  Абстрактный метод, который запускается
         *			при инициализации и деинициализации триггера
         */
        abstract protected void Init();
        abstract public void End();
    }
}