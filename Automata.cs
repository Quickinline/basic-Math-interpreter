using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Automata
    {
        public static string start(string input)
        {
            int i = 0;
            bool b = true;
            State Current = State.Start;

            while(i<input.Length && b)
            {

                Current = Transit(Current, input[i]);
                i++;
            }

            if(b && IsFinal(Current)) {return "Text Recognized";}
            else
            {
                if (i > 0)
                {
                    throw new AutoException("Syntaxe Error: ending in non-final state", Current, input[i - 1]);
                }
                else return null;
            }
        }


        private static State Transit (State state, char c)
        {
            if(!(IsNumber(c)||IsSymbol(c)|| c =='(' || c==')'))
            {
                throw new AutoException("Wrong Alphabet ....", state, c);
            }

            switch (state)
            {
                case State.Start:
                    if (IsNumber(c)) { return State.five; }
                    if (c == '(') { return State.one; }
                    if (IsPM(c)) { return State.six; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                    
                case State.one:
                    if (IsPM(c)) { return State.two; }
                    if (IsNumber(c)) { return State.three; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.two:
                    if (IsNumber(c)) { return State.three; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.three:
                    if (IsNumber(c)) { return State.three; }
                    if (c == ')') { return State.four; }
                    if (IsSymbol(c)) { return State.one; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.four:
                    if (IsSymbol(c)) { return State.Start; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.five:
                    if (IsNumber(c)) { return State.five; }
                    if (IsSymbol(c)) { return State.Start; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.six:
                    if (IsNumber(c)) { return State.seven; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                case State.seven:
                    if (IsNumber(c)) { return State.seven; }
                    if (IsSymbol(c)) { return State.Start; }
                    else
                    {
                        throw new AutoException("Automata Blocked", state, c);
                    }
                default:
                    throw new AutoException("Automata Blocked", state, c);
            }
        }

        private static bool IsSymbol (char c)
        {
            if(IsPM(c) || c=='*' || c == '/') { return true; }
            else
            { return false; }
        }
        private static bool IsNumber(char c)
        {
            if(c>= '0' && c <= '9') { return true; }
            else { return false; }
        }
        private static bool IsPM (char c)
        {
            if(c =='-' || c == '+') { return true; }
            else
            {
                return false;
            }
        }
        private static bool IsFinal(State state)
        {
            if (state == State.seven || state == State.five || state == State.four) { return true; }
            return false;
        }        
    }

    public enum State
    {
        Start, one, two, three, four, five, six, seven
    }

    public class AutoException : Exception
    {
        private State _current;
        private char _demanded;
        public char Demanded
        {
            get { return _demanded; }
        }
        public State Current
        {
            get { return _current; }
        }


        public AutoException(string message, State Current, char demanded) : base(message)
        {
            _current = Current;
            _demanded = demanded;
        }
    }
}
