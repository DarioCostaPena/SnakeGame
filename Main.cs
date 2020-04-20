using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame
{
    class SnakeGame : Snake.Listener {
        public static TiledWorld t = new TiledWorld(20, 10);
        public static Random random = new Random();
        public static char food = '·';

        static void Main() => new SnakeGame().Run();

        List<GenericMapObject> genericObjects = new List<GenericMapObject>();
        Snake snake = new Snake(10, 5, 5);

        private void Run() {
            snake.notifier.RegisterListener(this);

            AddApple();

            while (true) {
                Console.Clear();
                // draw walls
                t.DrawRectangle(0, 0, 20, 1, '#');
                t.DrawRectangle(0, 0, 1, 9, '#');
                t.DrawRectangle(0, 9, 20, 1, '#');
                t.DrawRectangle(19, 0, 1, 9, '#');

                foreach (var obj in genericObjects) {
                    obj.Draw();
                }

                snake.Draw();

                Console.Write(t.Update());
                Thread.Sleep(610);
            }
        }

        private void AddApple() {
            while (true) {
                var x = random.Next(18) + 1;
                var y = random.Next(8) + 1;

                if (t.Peek(x, y) != ' ') continue;

                genericObjects.FirstOrDefault(i => i.sprite == food).Let( result => {
                    if (result != null)
                        genericObjects.Remove(result);
                });

                genericObjects.Add(new GenericMapObject(x, y, food));

                break;
            }
        }

        public void AteApple(Snake snake) {
            AddApple();
        }
    }

    class GenericMapObject {
        public char sprite;
        
        public GenericMapObject(int x, int y, char sprite) {
            Position.Set(x, y);
            this.sprite = sprite;
        }
        
        public Vector2 Position { get => Position; set => Position = value; }

        public void Draw() {
            SnakeGame.t.Draw(Position.x, Position.y, sprite);
        }
    }
}