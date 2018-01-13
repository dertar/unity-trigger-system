/*
 * 	\brief Интерфейс для отладчика.
 * 			Для удобного использования понадобится
 * 			создать bool значения для отключения\включения 
 * 			отладчика одним кликом
 * 
 *  \code
 * 		public bool debug = true;
 * 		public void Log(string msg)
 *		{
 *			if(debug) Debug.Log (name + ".log: " + msg);
 *		}
*/
namespace Trigger
{
    interface IDebug
    {
        void Log(string msg);
    }
}