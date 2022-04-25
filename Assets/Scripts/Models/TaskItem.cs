using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class TaskItem : Item
    {
        public int id;
        public int type_id;
        public int energy_cost;
        public int wage;
        public int duration;
        public bool is_free;

        public override string ToString() => @$"Task:
        ID: {id}
        Type ID: {type_id}
        Title: {title}
        Is Free: {is_free}
        Energy Cost: {energy_cost}
        Wage: {wage}
        Duration: {duration}";
    }
}
