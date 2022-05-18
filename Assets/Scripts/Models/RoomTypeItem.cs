using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomTypeItem
    {
        public int id;
        public string title;
        public RoomTypeProperties properties;

        public string ToCaption() => $"{title}";

        public override string ToString() => @$"[room Type]:
title={title}
properties={properties}";
    }

    [System.Serializable]
    public class RoomTypeProperties
    {
        public UnityEngine.Color color;
        public Size size;
        public int durability;
        public List<Task> tasks;

        public override string ToString() => @$"[properties]:
color={color}
[size]:w={size.w},h={size.h}
durability={durability}
tasks={ListToString<Task>(tasks)}";

        private string ListToString<T>(List<T> items)
        {
            if (items.Count == 0)
            {
                return "—";
            }

            var result = "";
            foreach(var item in items)
            {
                result += $"{item},";
            }
            return result.Substring(0, result.Length-2);
        }
    }

    [System.Serializable]
    public class Size
    {
        public int w;
        public int h;
    }

    [System.Serializable]
    public class Task
    {
        public int type_id;
        public int count;

        public override string ToString() => @$"[task]:
count={count}
type id={type_id}";
    }
}
