using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class TaskItem : Item
    {
        public int type_id;
        public int organization_id;
        public int room_id;
        public int citizen_id;
        public int energy_cost;
        public int wage;
        public int duration;
        public bool is_free;

        public override string ToString() => @$"[task]:
id={id}
organization_id={organization_id}
room_id={room_id}
citizen_id={citizen_id}
type_id={type_id}
title={title}
is_free={is_free}
energy_cost={energy_cost}
wage={wage}
duration={duration}";
    }
}
