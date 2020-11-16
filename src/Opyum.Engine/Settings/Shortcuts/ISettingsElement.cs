namespace Opyum.Engine
{
    internal interface ISettingsElement<T>
    {
        /// <summary>
        /// Clone the given element
        /// </summary>
        /// <returns></returns>
        T Clone();

    }
}