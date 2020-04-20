using System.Collections.Generic;
using System;

namespace SnakeGame
{
    public class Snake {
        public interface Listener {
            void AteApple(Snake snake);
        }

        public Notifier<Listener> notifier = new Notifier<Listener>();
        List<Vector2> body;

        private Direction movingDirection = Direction.LEFT;
        public Direction MovingDirection {
            get => movingDirection; 
            set {
                if (!((value == Direction.DOWN && movingDirection == Direction.UP)
                    || (value == Direction.LEFT && movingDirection == Direction.RIGHT)
                    || (value == Direction.RIGHT && movingDirection == Direction.LEFT)
                    || (value == Direction.UP && movingDirection == Direction.DOWN))) {
                    movingDirection = value;
                }
            }
        }

        public Snake(int x, int y, int size) {
            body = new List<Vector2>();
            body.Add(new Vector2(x, y));

            for (int i = 1; i < size; i++) {
                body.Add(new Vector2(x + i, y));
            }
        }

        public void Draw() {
            AI();
            var newPos = new Vector2(body[0]);
            if (MovingDirection == Direction.DOWN) {
                newPos.y += 1;
            }
            if (MovingDirection == Direction.UP) {
                newPos.y -= 1;
            }
            if (MovingDirection == Direction.LEFT) {
                newPos.x -= 1;
            }
            if (MovingDirection == Direction.RIGHT) {
                newPos.x += 1;
            }

            if (!newPos.Equals(body[0])) {
                var previousBody = new List<Vector2>();

                body.ForEach(i => { previousBody.Add(new Vector2(i)); });

                for (int i = 1; i < body.Count; i++) {
                    body[i].Set(previousBody[i - 1]);
                }

                body[0].Set(newPos);
            }

            if (SnakeGame.t.Peek(body[0].x, body[0].y).Equals(SnakeGame.food)) {
                notifier.NotifyListeners(i => i.AteApple(this));

                body.Add(new Vector2(body[body.Count - 1]));
            }

            foreach (var v in body) {
                if (v == body[0])
                    SnakeGame.t.Draw(v.x, v.y, '@');
                else
                    SnakeGame.t.Draw(v.x, v.y, '~');
            }
        }

        private void AI() {
            Vector2 applePos = null;

            void searchPos() {
                for (int x = 1; x < SnakeGame.t.Width() - 1; x++) {
                    for (int y = 1; y < SnakeGame.t.Height() - 1; y++) {
                        if (SnakeGame.t.Peek(x, y) == SnakeGame.food) {
                            applePos = new Vector2(x, y);
                            return;
                        }
                    }
                }
            }

            searchPos();

            if (applePos != null) {
                
                if (body[0].x < applePos.x && SnakeGame.random.Next(3) == 0) {
                    MovingDirection = Direction.RIGHT;
                }

                if (body[0].y < applePos.y && SnakeGame.random.Next(3) == 1) {
                    MovingDirection = Direction.DOWN;
                }

                if (body[0].y > applePos.y && SnakeGame.random.Next(3) == 2) {
                    MovingDirection = Direction.UP;
                }

                if (body[0].x > applePos.x && SnakeGame.random.Next(3) == 2) {
                    MovingDirection = Direction.LEFT;
                }
            }
        }

        public enum Direction {
            UP, DOWN, LEFT, RIGHT
        }
    }
}