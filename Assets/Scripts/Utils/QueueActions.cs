namespace Trigger
{
    /* 
        \brief Вспомогательный класс для Trigger,
        данный класс управляет очередью из Action.
    */
    [System.Serializable]
    public class QueueActions {
        /// Массив из Action
        public Action[] actions;
        /// Индекс выполняющего Action
        private int index;

        public QueueActions()
        {
            Init();
        }

        /*
            \brief Инциализация, обнуления очереди
         */
        private void Init()
        {
            index = -1;
        }

        /*
            \brief Вытаскивает следующий Action из очереди,
                    если очередь закончилась, возвращает null.
            \return Action или null
         */
        public Action Next()
        {
            index++;

            return Current();
        }

        /*
            \brief Перезаряжает очередь, все Action ставятся 
                   статус "не запущен"
         */
        public void Recharge()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].SetState(ETriggerStates.NOT_ACTIVATED);
            }
            Init();
        }

        /*
            \brief Возвращает массив Action.
            \return Action[]
         */
        public Action[] GetActions()
        {
            return actions;
        }

        /*
            \brief Проверяет все ли Action закончили свою работу 
            \return bool
         */
        public bool IsAllActionsExecuted()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (!actions[i].IsState(ETriggerStates.FINISHED))
                    return false;
            }

            return true;
        }

        public Action Current()
        {
            if (actions == null || index < 0 || index >= actions.Length)
            {
                //Recharge ();
                return null;
            }

            return actions[index];
        }
    }
}