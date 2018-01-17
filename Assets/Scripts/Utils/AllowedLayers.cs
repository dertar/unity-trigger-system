using System.Collections;
using System.Collections.Generic;
namespace Trigger
{
    [System.Serializable]
    public class AllowedLayers
    {
        ///< Список разрешенных layers
        public List<int> list;

        /*
         *  \brief: Проверяет есть ли нужный элемент в списке
         *  \param[in] слой - int
         *  \return bool
         */
        public bool IsAllowed(int layer)
        {
            if (IsEmpty())
                return true;

            return list.Contains(layer);
        }

        /*
         * \todo: нужен нам этот метод?
         */

        public bool IsAllowed(int[] layers)
        {
            return false;
        }

        /*
         *  \brief: Проверяет список на пустоту
         *  \return bool
         */
        private bool IsEmpty()
        {
            return list != null && list.Count == 0;
        }
    }
}
