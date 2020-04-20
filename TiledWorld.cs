using System;

namespace SnakeGame
{
    public class TiledWorld {
        private Line[] lines;

        public TiledWorld(int width, int height) {
            ResetLines(width, height);
        }

        public void Draw(int x, int y, char c) {
            lines[y].columns[x] = c;
        }

        public void DrawRectangle(int x, int y, int width, int height, char c) {
            for (int xx = x; xx < x + width; xx++) {
                for (int yy = y; yy < y + height; yy++) {
                    lines[yy].columns[xx] = c;
                }
            }
        }

        public char Peek(int x, int y) {
            return lines[y].columns[x];
        }

        private void ResetLines(int width, int height) {
            lines =  new Line[height];
            for (int i = 0; i < lines.Length; i++) {
                lines[i] = new Line(width);
            }
        }

        public string Update() {
            string output = "";
            foreach (var l in lines) {
                foreach (var c in l.columns) {
                    output += c;
                }
                
                output += "\n";
            }

            ResetLines(lines[0].columns.Length, lines.Length);
            return output;
        }

        public class Line {
            int width;
            public char[] columns;

            public Line(int width) {
                this.width = width;
                this.columns = new char[width];
            }
        }
    }
}