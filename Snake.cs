using System.Collections.Generic;
using System;

namespace SnakeGame
{
    public class Snake {
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

            foreach (var v in body) {
                if (v == body[0])
                    SnakeGame.t.Draw(v.x, v.y, '@');
                else
                    SnakeGame.t.Draw(v.x, v.y, '~');
            }
        }

        public enum Direction {
            UP, DOWN, LEFT, RIGHT
        }
    }
}