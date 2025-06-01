using UnityEngine;

public static class InputServiceFactory
{
    public static IPlayerInputService CreateInputService()
    {
#if Unity_StandAlone
        return new KeyboardInputService();
#elif Unity_Android
        //...
#elif Unity_GamePad
        return new XboxGamepadInput();
#else
        //return new XboxGamepadInput();
        return new KeyboardInputService();
#endif
    }
}
