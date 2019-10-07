using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

namespace AI_BehaviourTree
{
    public enum BTState
    {
        Failure,
        Success,
        Continue,
        Abort
    }

    public static class BT
    {
        public static Root Root() { return new Root(); }
        public static Sequence Sequence() { return new Sequence(); }
        public static Selector Selector(bool shuffle = false) { return new Selector(shuffle); }
        public static Action Call(System.Action fn) { return new Action(fn); }
        public static ConditionalBranch If(System.Func<bool> fn) { return new ConditionalBranch(fn); }      
        public static While While(System.Func<bool> fn) { return new While(fn); }
        public static Condition_Method Condition_Method(System.Func<bool> fn) { return new Condition_Method(fn); }
        public static Condition_Variable Condition_Variable(bool a_bState) { return new Condition_Variable(a_bState); }
        public static Repeat Repeat(int count) { return new Repeat(count); }
        public static Wait Wait(float seconds) { return new Wait(seconds); }
        public static Terminate Terminate() { return new Terminate(); }
        public static RandomSequence RandomSequence(int[] weights = null) { return new AI_BehaviourTree.RandomSequence(weights); }
        public static RandomAction RandomAction(int[] a_arrPercent = null) { return new AI_BehaviourTree.RandomAction(a_arrPercent); }
    }

    public abstract class BTNode
    {
        public abstract BTState Tick();
    }

    public abstract class Branch : BTNode
    {
        protected int activeChild;
        protected List<BTNode> children = new List<BTNode>();
        public virtual Branch OpenBranch(params BTNode[] children)
        {
            for (var i = 0; i < children.Length; i++)
                this.children.Add(children[i]);
            return this;
        }

        public List<BTNode> Children()
        {
            return children;
        }

        public int ActiveChild()
        {
            return activeChild;
        }

        public virtual void ResetChildren()
        {
            activeChild = 0;
            for (var i = 0; i < children.Count; i++)
            {
                Branch b = children[i] as Branch;
                if (b != null)
                {
                    b.ResetChildren();
                }
            }
        }
    }

    public abstract class Decorator : BTNode
    {
        protected BTNode child;
        public Decorator Do(BTNode child)
        {
            this.child = child;
            return this;
        }
    }

    public class Sequence : Branch
    {
        public override BTState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BTState.Success:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    else
                        return BTState.Continue;
                case BTState.Failure:
                    activeChild = 0;
                    return BTState.Failure;
                case BTState.Continue:
                    return BTState.Continue;
                case BTState.Abort:
                    activeChild = 0;
                    return BTState.Abort;
            }
            throw new System.Exception("Tick Error");
        }
    }

    public class Selector : Branch
    {
        public Selector(bool shuffle)
        {
            if (shuffle)
            {
                var n = children.Count;
                while (n > 1)
                {
                    n--;
                    var k = Mathf.FloorToInt(Random.value * (n + 1));
                    var value = children[k];
                    children[k] = children[n];
                    children[n] = value;
                }
            }
        }

        public override BTState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BTState.Success:
                    activeChild = 0;
                    return BTState.Success;
                case BTState.Failure:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Failure;
                    }
                    else
                        return BTState.Continue;
                case BTState.Continue:
                    return BTState.Continue;
                case BTState.Abort:
                    activeChild = 0;
                    return BTState.Abort;
            }
            throw new System.Exception("Tick Error");
        }
    }

    public class Action : BTNode
    {
        System.Action fn;
        System.Func<IEnumerator<BTState>> coroutineFactory;
        IEnumerator<BTState> coroutine;
        public Action(System.Action fn)
        {
            this.fn = fn;
        }
        public Action(System.Func<IEnumerator<BTState>> coroutineFactory)
        {
            this.coroutineFactory = coroutineFactory;
        }
        public override BTState Tick()
        {
            if (fn != null)
            {
                fn();
                return BTState.Success;
            }
            else
            {
                if (coroutine == null)
                    coroutine = coroutineFactory();
                if (!coroutine.MoveNext())
                {
                    coroutine = null;
                    return BTState.Success;
                }
                var result = coroutine.Current;
                if (result == BTState.Continue)
                    return BTState.Continue;
                else
                {
                    coroutine = null;
                    return result;
                }
            }
        }

    }

    public class Condition_Method : BTNode
    {
        public System.Func<bool> fn;

        public Condition_Method(System.Func<bool> fn)
        {
            this.fn = fn;
        }
        public override BTState Tick()
        {
            return fn() ? BTState.Success : BTState.Failure;
        }
    }

    public class Condition_Variable : BTNode
    {
        public bool bState;

        public Condition_Variable(bool a_bState)
        {
            this.bState = a_bState;
        }
        public override BTState Tick()
        {
            return bState ? BTState.Success : BTState.Failure;
        }
    }

    public class ConditionalBranch : Block
    {
        public System.Func<bool> fn;
        bool tested = false;
        public ConditionalBranch(System.Func<bool> fn)
        {
            this.fn = fn;
        }
        public override BTState Tick()
        {
            if (!tested)
            {
                tested = fn();
            }
            if (tested)
            {
                var result = base.Tick();
                if (result == BTState.Continue)
                    return BTState.Continue;
                else
                {
                    tested = false;
                    return result;
                }
            }
            else
            {
                return BTState.Failure;
            }
        }
    }

    public class While : Block
    {
        public System.Func<bool> fn;

        public While(System.Func<bool> fn)
        {
            this.fn = fn;
        }

        public override BTState Tick()
        {
            if (fn())
                base.Tick();
            else
            {
                ResetChildren();
                return BTState.Failure;
            }

            return BTState.Continue;
        }
    }

    public abstract class Block : Branch
    {
        public override BTState Tick()
        {
            switch (children[activeChild].Tick())
            {
                case BTState.Continue:
                    return BTState.Continue;
                default:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;
            }
        }
    }

    public class Root : Block
    {
        public bool isTerminated = false;

        public override BTState Tick()
        {
            if (isTerminated) return BTState.Abort;
            while (true)
            {
                switch (children[activeChild].Tick())
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    case BTState.Abort:
                        isTerminated = true;
                        return BTState.Abort;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Success;
                        }
                        continue;
                }
            }
        }
    }

    public class Repeat : Block
    {
        public int count = 1;
        int currentCount = 0;
        public Repeat(int count)
        {
            this.count = count;
        }
        public override BTState Tick()
        {
            if (count > 0 && currentCount < count)
            {
                var result = base.Tick();
                switch (result)
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    default:
                        currentCount++;
                        if (currentCount == count)
                        {
                            currentCount = 0;
                            return BTState.Success;
                        }
                        return BTState.Continue;
                }
            }
            return BTState.Success;
        }
    }

    public class RandomAction : Block
    {
        int[] m_arrPercent = null;
      
        public RandomAction(int[] a_arrPercent = null)
        {
            activeChild = -1;

            m_arrPercent = a_arrPercent;
        }

        public override Branch OpenBranch(params BTNode[] children)
        {
            if (activeChild == -1)
            {
                activeChild = children.Length - 1;
            }

            return base.OpenBranch(children);
        }

        public override BTState Tick()
        {
            var result = children[activeChild].Tick();

            switch (result)
            {
                case BTState.Continue:
                    return BTState.Continue;
                default:
                    UpdateChild();
                    return result;
            }
        }

        void UpdateChild()
        {
            for(int i = 0; i < m_arrPercent.Length; ++i)
            {
                if (Rand.Percent(m_arrPercent[i]) == true)
                {
                    activeChild = i;
                }
            }          
        }
    }

    public class RandomSequence : Block
    {
        int[] m_Weight = null;
        int[] m_AddedWeight = null;

        public RandomSequence(int[] weight = null)
        {
            activeChild = -1;

            m_Weight = weight;
        }

        public override Branch OpenBranch(params BTNode[] children)
        {
            m_AddedWeight = new int[children.Length];

            for (int i = 0; i < children.Length; ++i)
            {
                int weight = 0;
                int previousWeight = 0;

                if (m_Weight == null || m_Weight.Length <= i)
                {
                    weight = 1;
                }
                else
                    weight = m_Weight[i];

                if (i > 0)
                    previousWeight = m_AddedWeight[i - 1];

                m_AddedWeight[i] = weight + previousWeight;
            }

            return base.OpenBranch(children);
        }

        public override BTState Tick()
        {
            if (activeChild == -1)
                PickNewChild();

            var result = children[activeChild].Tick();

            switch (result)
            {
                case BTState.Continue:
                    return BTState.Continue;
                default:
                    PickNewChild();
                    return result;
            }
        }

        void PickNewChild()
        {
            int choice = Random.Range(0, m_AddedWeight[m_AddedWeight.Length - 1]);

            for (int i = 0; i < m_AddedWeight.Length; ++i)
            {
                if (choice - m_AddedWeight[i] <= 0)
                {
                    activeChild = i;
                    break;
                }
            }
        }
    }

    public class Wait : BTNode
    {
        public float seconds = 0;
        float future = -1;
        public Wait(float seconds)
        {
            this.seconds = seconds;
        }

        public override BTState Tick()
        {
            if (future < 0)
                future = Time.time + seconds;

            if (Time.time >= future)
            {
                future = -1;
                return BTState.Success;
            }
            else
                return BTState.Continue;
        }

        public override string ToString()
        {
            return "Wait : " + (future - Time.time) + " / " + seconds;
        }
    }

    public class Terminate : BTNode
    {

        public override BTState Tick()
        {
            Debug.Log("터미네이트");
            return BTState.Abort;
        }

    }
}
