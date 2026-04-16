using CP2.Architecture;
using CP2.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP2.Controllers
{
    [Authorize]
    public class RoomsController(ILogger<RoomsController> logger, IRestProvider restProvider, IConfiguration configuration, IRoomsBusiness roomsBusiness) : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            Log(message: "RoomsController Index action invoked.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] string? code)
        {
            Log(message: "RoomsController Index action invoked.");
            EnsureProgressInitialized();

            if(AmIAdmin(room: 0, unlockRoom: 1, out ActionResult result))
                return result;

            // Room 0: if solved, unlock Room 1.
            var isSolved = await roomsBusiness.SolutionIndexAsync(code ?? string.Empty);
            MarkRoomAsSolved(0, isSolved);
            if (isSolved)
                UnlockNextRoom(1);

            return Json(new { isSolved });
        }

        public async Task<IActionResult> Room1()
        {
            if (!IsRoomUnlocked(1))
                return RedirectToCurrentUnlockedRoom();
            
            return View("Room1");
        }

        #region Room 1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room1(int x)
        {
            if (AmIAdmin(room: 1, unlockRoom: 2, out ActionResult result))
                return RedirectToAction("Room2");

            var isSolved = await roomsBusiness.SolutionRoom1Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(1, isSolved);
                UnlockNextRoom(2);
                return RedirectToAction("Room2");
            }

            return View();
        }

        #endregion

        #region Room 2
        [HttpGet]
        public async Task<IActionResult> Room2()
        {
            if (!IsRoomUnlocked(2))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room2(string x)
        {
            if (AmIAdmin(room: 2, unlockRoom: 3, out ActionResult result))
                return RedirectToAction("Room3");

            var isSolved = await roomsBusiness.SolutionRoom2Async(x);
            MarkRoomAsSolved(2, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(3);
                return RedirectToAction("Room3");
            }

            return View();
        }
        #endregion

        #region Room 3
        [HttpGet]
        public async Task<IActionResult> Room3()
        {
            if (!IsRoomUnlocked(3))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room3(string x)
        {
            if (AmIAdmin(room: 3, unlockRoom: 4, out ActionResult result))
                return RedirectToAction("Room4");

            var isSolved = await roomsBusiness.SolutionRoom3Async(x);
            MarkRoomAsSolved(3, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(4);
                return RedirectToAction("Room4");
            }

            return View();
        }
        #endregion

        #region Room 4
        [HttpGet]
        public async Task<IActionResult> Room4()
        {
            if (!IsRoomUnlocked(4))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room4(string x)
        {
            if (AmIAdmin(room: 4, unlockRoom: 5, out ActionResult result))
                return RedirectToAction("Room5");

            var isSolved = await roomsBusiness.SolutionRoom4Async(x);
            MarkRoomAsSolved(4, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(5);
                return RedirectToAction("Room5");
            }

            return View();
        }
        #endregion

        #region Room 5
        [HttpGet]
        public async Task<IActionResult> Room5()
        {
            if (!IsRoomUnlocked(5))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room5(string? x)
        {
            if (AmIAdmin(room: 5, unlockRoom: 6, out ActionResult result))
                return RedirectToAction("Room6");

            var isSolved = await roomsBusiness.SolutionRoom5Async();
            MarkRoomAsSolved(5, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(6);
                return RedirectToAction("Room6");
            }

            return View();
        }
        #endregion

        #region Room 6
        [HttpGet]
        public async Task<IActionResult> Room6()
        {
            if (!IsRoomUnlocked(6))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room6(int x)
        {
            if (AmIAdmin(room: 6, unlockRoom: 7, out ActionResult result))
              return RedirectToAction("Room7");

            var isSolved = await roomsBusiness.SolutionRoom6Async(x);
            MarkRoomAsSolved(6, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(7);
                return RedirectToAction("Room7");
            }

            return View();
        }
        #endregion

        #region Room 7
        [HttpGet]
        public async Task<IActionResult> Room7()
        {
            if (!IsRoomUnlocked(7))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room7(string x)
        {
            if (AmIAdmin(room: 7, unlockRoom: 8, out ActionResult result))
                return RedirectToAction("Room8");

            var isSolved = await roomsBusiness.SolutionRoom7Async(x);
            MarkRoomAsSolved(7, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(8);
                return RedirectToAction("Room8");
            }

            return View();
        }
        #endregion

        #region Room 8
        [HttpGet]
        public async Task<IActionResult> Room8()
        {
            if (!IsRoomUnlocked(8))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room8(string? x)
        {
            if (AmIAdmin(room: 8, unlockRoom: 9, out ActionResult result))
                return RedirectToAction("Room9");

            var isSolved = await roomsBusiness.SolutionRoom8Async();
            MarkRoomAsSolved(8, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(9);
                return RedirectToAction("Room9");
            }

            return View();
        }
        #endregion

        #region Room 9
        [HttpGet]
        public async Task<IActionResult> Room9()
        {
            if (!IsRoomUnlocked(9))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room9(string x)
        {
            if (AmIAdmin(room: 9, unlockRoom: 10, out ActionResult result))
                return RedirectToAction("Room10");

            var isSolved = await roomsBusiness.SolutionRoom9Async(x);
            MarkRoomAsSolved(9, isSolved);
            if (isSolved)
            {
                UnlockNextRoom(10);
                return RedirectToAction("Room10");
            }

            return View();
        }


        #endregion

        #region Room 10
        [HttpGet]
        public async Task<IActionResult> Room10()
        {
            if (!IsRoomUnlocked(10))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room10(string x)
        {
            if (AmIAdmin(room: 10, unlockRoom: 11, out ActionResult result))
                return RedirectToAction("Room11");

            var isSolved = await roomsBusiness.SolutionRoom10Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(10, isSolved);
                UnlockNextRoom(11);
                return RedirectToAction("Room11");
            }

            return View();
        }
        #endregion

        #region Room 11
        [HttpGet]
        public async Task<IActionResult> Room11()
        {
            if (!IsRoomUnlocked(11))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room11(string x)
        {
            if (AmIAdmin(room: 11, unlockRoom: 12, out ActionResult result))
                return RedirectToAction("Room12");

            var isSolved = await roomsBusiness.SolutionRoom11Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(11, isSolved);
                UnlockNextRoom(12);
                return RedirectToAction("Room12");
            }

            return View();
        }
        #endregion

        #region Room 12
        [HttpGet]
        public async Task<IActionResult> Room12()
        {
            if (!IsRoomUnlocked(12))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room12(string x)
        {
            if (AmIAdmin(room: 12, unlockRoom: 13, out ActionResult result))
                return RedirectToAction("Room13");

            var isSolved = await roomsBusiness.SolutionRoom12Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(12, isSolved);
                UnlockNextRoom(13);
                return RedirectToAction("Room13");
            }

            return View();
        }
        #endregion

        #region Room 13
        [HttpGet]
        public async Task<IActionResult> Room13()
        {
            if (!IsRoomUnlocked(13))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room13(string x)
        {
            if (AmIAdmin(room: 13, unlockRoom: 14, out ActionResult result))
                return RedirectToAction("Room14");

            var isSolved = await roomsBusiness.SolutionRoom13Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(13, isSolved);
                UnlockNextRoom(14);
                return RedirectToAction("Room14");
            }

            return View();
        }
        #endregion

        #region Room 14
        [HttpGet]
        public async Task<IActionResult> Room14()
        {
            if (!IsRoomUnlocked(14))
            {
                return RedirectToCurrentUnlockedRoom();
            }

            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room14(string x)
        {
            if (AmIAdmin(room: 14, unlockRoom: 15, out ActionResult result))
                return RedirectToAction("Room15");

            var isSolved = await roomsBusiness.SolutionRoom14Async(x);
            if (isSolved)
            {
                MarkRoomAsSolved(14, isSolved);
                UnlockNextRoom(15);
                return RedirectToAction("Room15");
            }

            return View();
        }
        #endregion

        #region Exit
        [HttpGet]

        public async Task<IActionResult> Room15()
        {
            if (!IsRoomUnlocked(15))
            {
                return RedirectToCurrentUnlockedRoom();
            }
            await Task.CompletedTask;
            return View("Exit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Room15(string x)
        {
            if (AmIAdmin(room: 15, unlockRoom: 15, out ActionResult result))
                return View("Exit");

            var isSolved = await roomsBusiness.CanExitTheRoomsAsync(x);

            await Task.CompletedTask;
            MarkRoomAsSolved(15, isSolved);
            if (isSolved)
            {
                return View("Exit");
            }

            return View();
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> End()
        {
            await Task.CompletedTask;
            return View("End");
        }
    }
}
