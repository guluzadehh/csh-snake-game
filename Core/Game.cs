// namespace SnakeGame.Core;

// public abstract class Game<T>
// {
//     protected readonly List<IDrawable> _drawables = [];
//     protected List<KeyHandler<T>> _keyEventHandlers = [];

//     protected virtual int SleepTime { get; set; } = 250;

//     public void Start()
//     {
//         Console.Clear();

//         while (true)
//         {
//             bool canContinue = FrameLogic();

//             UpdateFrame();
//             if (!canContinue) break;

//             Thread.Sleep(SleepTime);
//         }

//         Console.SetCursorPosition(WIDTH + GamePosition.X, HEIGHT + GamePosition.Y);
//         Console.WriteLine();
//     }

//     protected abstract bool FrameLogic();


//     protected virtual void UpdateFrame()
//     {
//         Console.Clear();
//         foreach (IDrawable drawable in _drawables)
//         {
//             drawable.Draw();
//         }
//     }

//     protected void ListenForKeys()
//     {
//         if (Console.KeyAvailable)
//         {
//             ConsoleKey key = Console.ReadKey(true).Key;

//             foreach (KeyHandler keyHandler in _keyEventHandlers)
//             {
//                 if (keyHandler.ListensFor(key))
//                 {
//                     keyHandler.Run(this);
//                 }
//             }
//         }
//     }

//     protected abstract List<KeyHandler<T>> InitKeyHandlers();
// }