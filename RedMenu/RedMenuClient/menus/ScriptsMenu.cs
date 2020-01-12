using System;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace RedMenuClient.menus
{
    class ScriptMenu : BaseScript
    {
        private static Menu menu = new Menu("Scripts Menu", "Activate Scripts");
        private static bool setupDone = false;
        public static bool PingBool { get; private set; } = false;
        public static bool FPSBool { get; private set; } = false;
        public static bool PlayerCountBool { get; private set; } = false;
        public static int showping = 0;
        public static int showfps = 0;
        public static dynamic prevtime = GetFrameCount();
        public static dynamic prevframes = GetGameTimer();



        public ScriptMenu()
        {
            EventHandlers["RedMenu:PingResult"] += new Action<int>((ping) =>
            {
                showping = ping;
                //Debug.WriteLine($"{showping}");
            });
        }

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuCheckboxItem pingBox = new MenuCheckboxItem("Display Ping", "Displays your current Ping.", PingBool);
            MenuCheckboxItem fpsBox = new MenuCheckboxItem("Display FPS", "Displays your current FPS.", FPSBool);
            MenuCheckboxItem playerCountBox = new MenuCheckboxItem("Display Player Count", "Displays the server's player count.", PlayerCountBool);
            menu.AddMenuItem(pingBox);
            menu.AddMenuItem(fpsBox);
            menu.AddMenuItem(playerCountBox);

            menu.OnCheckboxChange += async (sender, item, index, _checked) =>
            {
                if (item == pingBox)
                {
                    PingBool = _checked;
                    while (PingBool == true)
                    {
                        TriggerServerEvent("RedMenu:GetPing");
                        DisplayText(0.85f, 0.86f, $"{showping} MS", 88, 0, 0, 255, 0.88f, 0.88f);
                        await Delay(0);
                    }
                }
                else if (item == fpsBox)
                {
                    FPSBool = _checked;
                    while (FPSBool == true)
                    {
                        var curtime = GetGameTimer();
                        var curframes = GetFrameCount();
                        if (curtime - prevtime > 1000)
                        {
                            showfps = curframes - prevframes - 1;
                            prevtime = curtime;
                            prevframes = curframes;
                        }
                        DisplayText(0.85f, 0.90f, $"{showfps} FPS", 88, 0, 0, 255, 0.88f, 0.88f);
                        await Delay(0);
                    }
                }
                else if (item == playerCountBox)
                {
                    PlayerCountBool = _checked;
                    while (PlayerCountBool == true)
                    {
                        object playerCount = GetActivePlayers().Count;
                        DisplayText(0.80f, 0.94f, $"Players: {playerCount} / 32", 88, 0, 0, 255, 0.88f, 0.88f);
                        await Delay(1);
                    }
                }
            };
        }

        private static void DisplayText(float x, float y, dynamic theText, int r, int g, int b, int opacity, float scaleX, float scaleY)
        {
            SetTextScale(scaleX, scaleY);
            SetTextColor(r, g, b, opacity);
            Function.Call((Hash)0xADA9255D, 10); //Font
            Function.Call(Hash._DISPLAY_TEXT, Function.Call<long>(Hash._CREATE_VAR_STRING, 10, "LITERAL_STRING", theText), x, y); //display text
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}