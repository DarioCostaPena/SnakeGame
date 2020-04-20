using System;
using System.Threading;
using System.Collections.Generic;

namespace SnakeGame
{
    class SnakeGame {
        public static TiledWorld t = new TiledWorld(20, 10);
        public static Random random = new Random();

        static void Main() {
            var genericObjects = new List<GenericMapObject>();
            var snake = new Snake(10, 5, 5);

            var food = '·';

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
    }

    class GenericMapObject {
        private char sprite;
        Vector2 position = new Vector2();

        public GenericMapObject(int x, int y, char sprite) {
            position.Set(x, y);
            this.sprite = sprite;
        }

        public void Draw() {
            SnakeGame.t.Draw(position.x, position.y, sprite);
        }
    }

    class Vector2 {
        public int x;
        public int y;

        public Vector2(int x = 0, int y = 0) {
            Set(x, y);
        }

        public Vector2(Vector2 vector) {
            Set(vector);
        }

        public void Set(int x = 0, int y = 0) {
            this.x = x;
            this.y = y;
        }

        public void Set(Vector2 vector) {
            this.Set(vector.x, vector.y);
        }
    }
}