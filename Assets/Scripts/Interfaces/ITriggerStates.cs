namespace Trigger
{
    interface ITriggerStates
    {
        void SetState(ETriggerStates state);
        bool IsState(ETriggerStates state);
    }
}
