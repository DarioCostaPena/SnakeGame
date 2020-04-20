using System;
using System.Collections.Generic;

namespace SnakeGame {
    public class Vector2 {
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

    public class Notifier<T> {
        private List<T> listeners = new List<T>();

        public void RegisterListener(T listener) {
            listeners.Add(listener);
        }

        public void RemoveListener(T listener) {
            listeners.Remove(listener);
        }

        public void NotifyListeners(Action<T> function) {
            foreach (var listener in listeners) function(listener);
        }
    }

    static class Utils {
        // Kotlin: fun <T, R> T.let(block: (T) -> R): R
        public static R Let<T, R>(this T self, Func<T, R> block) 
        {
            return block(self);
        }

        // Kotlin: fun <T, R> T.let(block: (T) -> R): R
        public static void Let<R>(this R self, Action<R> block) 
        {
            block(self);
        }

        // Kotlin: fun <T> T.also(block: (T) -> Unit): T
        public static T Also<T>(this T self, Action<T> block)
        {
            block(self);
            return self;
        }   
    }
}