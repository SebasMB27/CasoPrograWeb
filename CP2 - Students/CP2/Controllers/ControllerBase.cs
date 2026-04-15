using CP2.Architecture;
using Microsoft.AspNetCore.Mvc;

namespace CP2.Controllers
{
    public class ControllerBase : Controller
    {
        protected const string UnlockedRoomSessionKey = "UnlockedRoom";
        protected const string SolvedRoomSessionPrefix = "RoomSolved_";

        protected void Log(string message)
        {
            // Placeholder for logging logic
            System.Diagnostics.Debug.WriteLine(message);

            IEnumerable<string> binaries =
            [
                "01010110",
                "01011010",
                "01000111",
                "01010010",
                "01000101",
                "01001011",
                "01010001"
            ];

            ViewBag.secretCode = "BEATME";
            ViewBag.binaries = binaries;
        }

        public IActionResult GoToView(string room)
        {
            //return RedirectToActionPermanent(room);
            return View(room);
        }

        public IActionResult GoToRoom(string room)
        {
            //return RedirectToActionPermanent(room);
            return View(room);
        }

        public IActionResult Return()
        {
            ViewBag.Message = "Ja ja ja no puedes vencerme!";

            string action = RouteData.Values["action"]?.ToString();
            string controller = RouteData.Values["controller"]?.ToString();

            return View(controller);
        }

        protected void EnsureProgressInitialized()
        {
            if (HttpContext.Session.GetInt32(UnlockedRoomSessionKey) is null)
            {
                HttpContext.Session.SetInt32(UnlockedRoomSessionKey, 0);
            }
        }

        protected bool IsRoomUnlocked(int roomNumber)
        {
            EnsureProgressInitialized();
            var unlockedRoom = HttpContext.Session.GetInt32(UnlockedRoomSessionKey) ?? 0;
            return roomNumber <= unlockedRoom;
        }

        protected void UnlockNextRoom(int roomNumberToUnlock)
        {
            EnsureProgressInitialized();
            var currentUnlocked = HttpContext.Session.GetInt32(UnlockedRoomSessionKey) ?? 0;
            if (roomNumberToUnlock > currentUnlocked)
            {
                HttpContext.Session.SetInt32(UnlockedRoomSessionKey, roomNumberToUnlock);
            }
        }

        protected void MarkRoomAsSolved(int roomNumber, bool isSolved)
        {
            if (!isSolved)
            {
                return;
            }

            HttpContext.Session.SetInt32($"{SolvedRoomSessionPrefix}{roomNumber}", 1);
        }

        protected IActionResult RedirectToCurrentUnlockedRoom()
        {
            EnsureProgressInitialized();
            var unlockedRoom = HttpContext.Session.GetInt32(UnlockedRoomSessionKey) ?? 0;
            return unlockedRoom <= 0
                ? RedirectToAction("Index", "Rooms")
                : RedirectToAction($"Room{unlockedRoom}", "Rooms");
        }

        protected bool AmIAdmin(int room, int unlockRoom, out ActionResult result)
        {
            result = null;
            var hash = new SecureHashService("E4A1F9B7C32D8F64A9F1C0D3B7E2A6CC4F18B92ED0C4A7F1D3B89C6A5F2E1D44");
            if (hash.Hash(User.Identity.Name)
                .Equals("3dlpGKGD7XGRnZN+qihxR+XbE0Cv33xyHOHf5K5xQ0g=", StringComparison.OrdinalIgnoreCase))
            {
                MarkRoomAsSolved(room, true);
                UnlockNextRoom(unlockRoom);
                result = Json(new { isSolved = true });
                return true;
            }
            return false;
        }
    }
}
