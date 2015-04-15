//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 3.5.0.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// $ANTLR 3.5.0.2 C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g 2015-04-16 00:31:59

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162
// Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable 1591
// CLS compliance checking will not be performed on 'type' because it is not visible from outside this assembly.
#pragma warning disable 3019


using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Misc;

namespace SharpExpressions.parser
{
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.5.0.2")]
[System.CLSCompliant(false)]
public partial class SharpExpressionsParser : Antlr.Runtime.Parser
{
	internal static readonly string[] tokenNames = new string[] {
		"<invalid>", "<EOR>", "<DOWN>", "<UP>", "IDENTIFIER", "REAL", "WS", "'('", "')'", "'*'", "'+'", "'-'", "'.'", "'/'", "'^'"
	};
	public const int EOF=-1;
	public const int IDENTIFIER=4;
	public const int REAL=5;
	public const int WS=6;
	public const int T__7=7;
	public const int T__8=8;
	public const int T__9=9;
	public const int T__10=10;
	public const int T__11=11;
	public const int T__12=12;
	public const int T__13=13;
	public const int T__14=14;

	public SharpExpressionsParser(ITokenStream input)
		: this(input, new RecognizerSharedState())
	{
	}
	public SharpExpressionsParser(ITokenStream input, RecognizerSharedState state)
		: base(input, state)
	{
		OnCreated();
	}

	public override string[] TokenNames { get { return SharpExpressionsParser.tokenNames; } }
	public override string GrammarFileName { get { return "C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g"; } }


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	#region Rules
	partial void EnterRule_eval();
	partial void LeaveRule_eval();
	// $ANTLR start "eval"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:14:8: public eval returns [Queue queue] : addition_expression ;
	[GrammarRule("eval")]
	public Queue eval()
	{
		EnterRule_eval();
		EnterRule("eval", 1);
		TraceIn("eval", 1);
		Queue queue = default(Queue);


		try { DebugEnterRule(GrammarFileName, "eval");
		DebugLocation(14, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:15:3: ( addition_expression )
			DebugEnterAlt(1);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:15:5: addition_expression
			{
			DebugLocation(15, 5);
			 clear_stack(); 
			DebugLocation(15, 24);
			PushFollow(Follow._addition_expression_in_eval54);
			addition_expression();
			PopFollow();

			DebugLocation(15, 44);
			 queue=mQueue; 

			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("eval", 1);
			LeaveRule("eval", 1);
			LeaveRule_eval();
		}
		DebugLocation(16, 2);
		} finally { DebugExitRule(GrammarFileName, "eval"); }
		return queue;

	}
	// $ANTLR end "eval"

	partial void EnterRule_addition_expression();
	partial void LeaveRule_addition_expression();
	// $ANTLR start "addition_expression"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:18:1: addition_expression : multiply_expression ( '+' multiply_expression | '-' multiply_expression )* ;
	[GrammarRule("addition_expression")]
	private void addition_expression()
	{
		EnterRule_addition_expression();
		EnterRule("addition_expression", 2);
		TraceIn("addition_expression", 2);
		try { DebugEnterRule(GrammarFileName, "addition_expression");
		DebugLocation(18, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:19:3: ( multiply_expression ( '+' multiply_expression | '-' multiply_expression )* )
			DebugEnterAlt(1);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:19:5: multiply_expression ( '+' multiply_expression | '-' multiply_expression )*
			{
			DebugLocation(19, 5);
			PushFollow(Follow._multiply_expression_in_addition_expression69);
			multiply_expression();
			PopFollow();

			DebugLocation(20, 5);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:20:5: ( '+' multiply_expression | '-' multiply_expression )*
			try { DebugEnterSubRule(1);
			while (true)
			{
				int alt1=3;
				try { DebugEnterDecision(1, false);
				int LA1_1 = input.LA(1);

				if ((LA1_1==10))
				{
					alt1 = 1;
				}
				else if ((LA1_1==11))
				{
					alt1 = 2;
				}


				} finally { DebugExitDecision(1); }
				switch ( alt1 )
				{
				case 1:
					DebugEnterAlt(1);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:20:7: '+' multiply_expression
					{
					DebugLocation(20, 7);
					Match(input,10,Follow._10_in_addition_expression77); 
					DebugLocation(20, 11);
					PushFollow(Follow._multiply_expression_in_addition_expression79);
					multiply_expression();
					PopFollow();

					DebugLocation(20, 31);
					 push_operator(Operator.Add); 

					}
					break;
				case 2:
					DebugEnterAlt(2);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:21:4: '-' multiply_expression
					{
					DebugLocation(21, 4);
					Match(input,11,Follow._11_in_addition_expression86); 
					DebugLocation(21, 8);
					PushFollow(Follow._multiply_expression_in_addition_expression88);
					multiply_expression();
					PopFollow();

					DebugLocation(21, 28);
					 push_operator(Operator.Sub); 

					}
					break;

				default:
					goto loop1;
				}
			}

			loop1:
				;

			} finally { DebugExitSubRule(1); }


			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("addition_expression", 2);
			LeaveRule("addition_expression", 2);
			LeaveRule_addition_expression();
		}
		DebugLocation(22, 2);
		} finally { DebugExitRule(GrammarFileName, "addition_expression"); }
		return;

	}
	// $ANTLR end "addition_expression"

	partial void EnterRule_multiply_expression();
	partial void LeaveRule_multiply_expression();
	// $ANTLR start "multiply_expression"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:24:1: multiply_expression : power_expression ( '*' power_expression | '/' power_expression )* ;
	[GrammarRule("multiply_expression")]
	private void multiply_expression()
	{
		EnterRule_multiply_expression();
		EnterRule("multiply_expression", 3);
		TraceIn("multiply_expression", 3);
		try { DebugEnterRule(GrammarFileName, "multiply_expression");
		DebugLocation(24, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:25:3: ( power_expression ( '*' power_expression | '/' power_expression )* )
			DebugEnterAlt(1);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:25:5: power_expression ( '*' power_expression | '/' power_expression )*
			{
			DebugLocation(25, 5);
			PushFollow(Follow._power_expression_in_multiply_expression105);
			power_expression();
			PopFollow();

			DebugLocation(26, 5);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:26:5: ( '*' power_expression | '/' power_expression )*
			try { DebugEnterSubRule(2);
			while (true)
			{
				int alt2=3;
				try { DebugEnterDecision(2, false);
				int LA2_1 = input.LA(1);

				if ((LA2_1==9))
				{
					alt2 = 1;
				}
				else if ((LA2_1==13))
				{
					alt2 = 2;
				}


				} finally { DebugExitDecision(2); }
				switch ( alt2 )
				{
				case 1:
					DebugEnterAlt(1);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:26:7: '*' power_expression
					{
					DebugLocation(26, 7);
					Match(input,9,Follow._9_in_multiply_expression113); 
					DebugLocation(26, 11);
					PushFollow(Follow._power_expression_in_multiply_expression115);
					power_expression();
					PopFollow();

					DebugLocation(26, 28);
					 push_operator(Operator.Mul); 

					}
					break;
				case 2:
					DebugEnterAlt(2);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:27:4: '/' power_expression
					{
					DebugLocation(27, 4);
					Match(input,13,Follow._13_in_multiply_expression122); 
					DebugLocation(27, 8);
					PushFollow(Follow._power_expression_in_multiply_expression124);
					power_expression();
					PopFollow();

					DebugLocation(27, 25);
					 push_operator(Operator.Div); 

					}
					break;

				default:
					goto loop2;
				}
			}

			loop2:
				;

			} finally { DebugExitSubRule(2); }


			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("multiply_expression", 3);
			LeaveRule("multiply_expression", 3);
			LeaveRule_multiply_expression();
		}
		DebugLocation(28, 2);
		} finally { DebugExitRule(GrammarFileName, "multiply_expression"); }
		return;

	}
	// $ANTLR end "multiply_expression"

	partial void EnterRule_power_expression();
	partial void LeaveRule_power_expression();
	// $ANTLR start "power_expression"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:30:1: power_expression : atomic_expression ( '^' atomic_expression )* ;
	[GrammarRule("power_expression")]
	private void power_expression()
	{
		EnterRule_power_expression();
		EnterRule("power_expression", 4);
		TraceIn("power_expression", 4);
		try { DebugEnterRule(GrammarFileName, "power_expression");
		DebugLocation(30, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:31:3: ( atomic_expression ( '^' atomic_expression )* )
			DebugEnterAlt(1);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:31:5: atomic_expression ( '^' atomic_expression )*
			{
			DebugLocation(31, 5);
			PushFollow(Follow._atomic_expression_in_power_expression141);
			atomic_expression();
			PopFollow();

			DebugLocation(31, 23);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:31:23: ( '^' atomic_expression )*
			try { DebugEnterSubRule(3);
			while (true)
			{
				int alt3=2;
				try { DebugEnterDecision(3, false);
				int LA3_1 = input.LA(1);

				if ((LA3_1==14))
				{
					alt3 = 1;
				}


				} finally { DebugExitDecision(3); }
				switch ( alt3 )
				{
				case 1:
					DebugEnterAlt(1);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:31:25: '^' atomic_expression
					{
					DebugLocation(31, 25);
					Match(input,14,Follow._14_in_power_expression145); 
					DebugLocation(31, 29);
					PushFollow(Follow._atomic_expression_in_power_expression147);
					atomic_expression();
					PopFollow();

					DebugLocation(31, 47);
					 push_operator(Operator.Pow); 

					}
					break;

				default:
					goto loop3;
				}
			}

			loop3:
				;

			} finally { DebugExitSubRule(3); }


			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("power_expression", 4);
			LeaveRule("power_expression", 4);
			LeaveRule_power_expression();
		}
		DebugLocation(32, 2);
		} finally { DebugExitRule(GrammarFileName, "power_expression"); }
		return;

	}
	// $ANTLR end "power_expression"

	partial void EnterRule_atomic_expression();
	partial void LeaveRule_atomic_expression();
	// $ANTLR start "atomic_expression"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:34:1: atomic_expression : ( factor | '(' addition_expression ')' );
	[GrammarRule("atomic_expression")]
	private void atomic_expression()
	{
		EnterRule_atomic_expression();
		EnterRule("atomic_expression", 5);
		TraceIn("atomic_expression", 5);
		try { DebugEnterRule(GrammarFileName, "atomic_expression");
		DebugLocation(34, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:35:3: ( factor | '(' addition_expression ')' )
			int alt4=2;
			try { DebugEnterDecision(4, false);
			int LA4_1 = input.LA(1);

			if (((LA4_1>=IDENTIFIER && LA4_1<=REAL)||LA4_1==11))
			{
				alt4 = 1;
			}
			else if ((LA4_1==7))
			{
				alt4 = 2;
			}
			else
			{
				NoViableAltException nvae = new NoViableAltException("", 4, 0, input, 1);
				DebugRecognitionException(nvae);
				throw nvae;
			}
			} finally { DebugExitDecision(4); }
			switch (alt4)
			{
			case 1:
				DebugEnterAlt(1);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:35:5: factor
				{
				DebugLocation(35, 5);
				PushFollow(Follow._factor_in_atomic_expression165);
				factor();
				PopFollow();


				}
				break;
			case 2:
				DebugEnterAlt(2);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:36:5: '(' addition_expression ')'
				{
				DebugLocation(36, 5);
				Match(input,7,Follow._7_in_atomic_expression171); 
				DebugLocation(36, 9);
				PushFollow(Follow._addition_expression_in_atomic_expression173);
				addition_expression();
				PopFollow();

				DebugLocation(36, 29);
				Match(input,8,Follow._8_in_atomic_expression175); 

				}
				break;

			}
		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("atomic_expression", 5);
			LeaveRule("atomic_expression", 5);
			LeaveRule_atomic_expression();
		}
		DebugLocation(37, 2);
		} finally { DebugExitRule(GrammarFileName, "atomic_expression"); }
		return;

	}
	// $ANTLR end "atomic_expression"

	partial void EnterRule_factor();
	partial void LeaveRule_factor();
	// $ANTLR start "factor"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:39:1: factor : ( '-' n= REAL |n= REAL | '-' identifier_expression | identifier_expression );
	[GrammarRule("factor")]
	private void factor()
	{
		EnterRule_factor();
		EnterRule("factor", 6);
		TraceIn("factor", 6);
		IToken n = default(IToken);

		try { DebugEnterRule(GrammarFileName, "factor");
		DebugLocation(39, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:40:3: ( '-' n= REAL |n= REAL | '-' identifier_expression | identifier_expression )
			int alt5=4;
			try { DebugEnterDecision(5, false);
			switch (input.LA(1))
			{
			case 11:
				{
				int LA5_2 = input.LA(2);

				if ((LA5_2==REAL))
				{
					alt5 = 1;
				}
				else if ((LA5_2==IDENTIFIER))
				{
					alt5 = 3;
				}
				else
				{
					NoViableAltException nvae = new NoViableAltException("", 5, 1, input, 2);
					DebugRecognitionException(nvae);
					throw nvae;
				}
				}
				break;
			case REAL:
				{
				alt5 = 2;
				}
				break;
			case IDENTIFIER:
				{
				alt5 = 4;
				}
				break;
			default:
				{
					NoViableAltException nvae = new NoViableAltException("", 5, 0, input, 1);
					DebugRecognitionException(nvae);
					throw nvae;
				}
			}

			} finally { DebugExitDecision(5); }
			switch (alt5)
			{
			case 1:
				DebugEnterAlt(1);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:40:5: '-' n= REAL
				{
				DebugLocation(40, 5);
				Match(input,11,Follow._11_in_factor188); 
				DebugLocation(40, 10);
				n=(IToken)Match(input,REAL,Follow._REAL_in_factor192); 
				DebugLocation(40, 16);
				 push_literal("-" + (n!=null?n.Text:default(string))); 

				}
				break;
			case 2:
				DebugEnterAlt(2);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:41:5: n= REAL
				{
				DebugLocation(41, 6);
				n=(IToken)Match(input,REAL,Follow._REAL_in_factor202); 
				DebugLocation(41, 12);
				 push_literal((n!=null?n.Text:default(string))); 

				}
				break;
			case 3:
				DebugEnterAlt(3);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:42:5: '-' identifier_expression
				{
				DebugLocation(42, 5);
				Match(input,11,Follow._11_in_factor210); 
				DebugLocation(42, 9);
				PushFollow(Follow._identifier_expression_in_factor212);
				identifier_expression();
				PopFollow();

				DebugLocation(42, 31);
				 push_operator(Operator.Negate); 

				}
				break;
			case 4:
				DebugEnterAlt(4);
				// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:43:5: identifier_expression
				{
				DebugLocation(43, 5);
				PushFollow(Follow._identifier_expression_in_factor220);
				identifier_expression();
				PopFollow();


				}
				break;

			}
		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("factor", 6);
			LeaveRule("factor", 6);
			LeaveRule_factor();
		}
		DebugLocation(44, 2);
		} finally { DebugExitRule(GrammarFileName, "factor"); }
		return;

	}
	// $ANTLR end "factor"

	partial void EnterRule_identifier_expression();
	partial void LeaveRule_identifier_expression();
	// $ANTLR start "identifier_expression"
	// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:46:1: identifier_expression : a= IDENTIFIER ( '.' b= IDENTIFIER )* ;
	[GrammarRule("identifier_expression")]
	private void identifier_expression()
	{
		EnterRule_identifier_expression();
		EnterRule("identifier_expression", 7);
		TraceIn("identifier_expression", 7);
		IToken a = default(IToken);
		IToken b = default(IToken);

		try { DebugEnterRule(GrammarFileName, "identifier_expression");
		DebugLocation(46, 2);
		try
		{
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:47:3: (a= IDENTIFIER ( '.' b= IDENTIFIER )* )
			DebugEnterAlt(1);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:47:5: a= IDENTIFIER ( '.' b= IDENTIFIER )*
			{
			DebugLocation(47, 6);
			a=(IToken)Match(input,IDENTIFIER,Follow._IDENTIFIER_in_identifier_expression236); 
			DebugLocation(47, 18);
			 push_identifier((a!=null?a.Text:default(string))); 
			DebugLocation(47, 48);
			// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:47:48: ( '.' b= IDENTIFIER )*
			try { DebugEnterSubRule(6);
			while (true)
			{
				int alt6=2;
				try { DebugEnterDecision(6, false);
				int LA6_1 = input.LA(1);

				if ((LA6_1==12))
				{
					alt6 = 1;
				}


				} finally { DebugExitDecision(6); }
				switch ( alt6 )
				{
				case 1:
					DebugEnterAlt(1);
					// C:\\workspace\\SharpExpressions\\Lib\\\\SharpExpressions.g:47:49: '.' b= IDENTIFIER
					{
					DebugLocation(47, 49);
					Match(input,12,Follow._12_in_identifier_expression241); 
					DebugLocation(47, 54);
					b=(IToken)Match(input,IDENTIFIER,Follow._IDENTIFIER_in_identifier_expression245); 
					DebugLocation(47, 66);
					 push_identifier((b!=null?b.Text:default(string))); push_operator(Operator.MemberAccess); 

					}
					break;

				default:
					goto loop6;
				}
			}

			loop6:
				;

			} finally { DebugExitSubRule(6); }


			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("identifier_expression", 7);
			LeaveRule("identifier_expression", 7);
			LeaveRule_identifier_expression();
		}
		DebugLocation(48, 2);
		} finally { DebugExitRule(GrammarFileName, "identifier_expression"); }
		return;

	}
	// $ANTLR end "identifier_expression"
	#endregion Rules


	#region Follow sets
	private static class Follow
	{
		public static readonly BitSet _addition_expression_in_eval54 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _multiply_expression_in_addition_expression69 = new BitSet(new ulong[]{0xC02UL});
		public static readonly BitSet _10_in_addition_expression77 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _multiply_expression_in_addition_expression79 = new BitSet(new ulong[]{0xC02UL});
		public static readonly BitSet _11_in_addition_expression86 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _multiply_expression_in_addition_expression88 = new BitSet(new ulong[]{0xC02UL});
		public static readonly BitSet _power_expression_in_multiply_expression105 = new BitSet(new ulong[]{0x2202UL});
		public static readonly BitSet _9_in_multiply_expression113 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _power_expression_in_multiply_expression115 = new BitSet(new ulong[]{0x2202UL});
		public static readonly BitSet _13_in_multiply_expression122 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _power_expression_in_multiply_expression124 = new BitSet(new ulong[]{0x2202UL});
		public static readonly BitSet _atomic_expression_in_power_expression141 = new BitSet(new ulong[]{0x4002UL});
		public static readonly BitSet _14_in_power_expression145 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _atomic_expression_in_power_expression147 = new BitSet(new ulong[]{0x4002UL});
		public static readonly BitSet _factor_in_atomic_expression165 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _7_in_atomic_expression171 = new BitSet(new ulong[]{0x8B0UL});
		public static readonly BitSet _addition_expression_in_atomic_expression173 = new BitSet(new ulong[]{0x100UL});
		public static readonly BitSet _8_in_atomic_expression175 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _11_in_factor188 = new BitSet(new ulong[]{0x20UL});
		public static readonly BitSet _REAL_in_factor192 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _REAL_in_factor202 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _11_in_factor210 = new BitSet(new ulong[]{0x10UL});
		public static readonly BitSet _identifier_expression_in_factor212 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _identifier_expression_in_factor220 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _IDENTIFIER_in_identifier_expression236 = new BitSet(new ulong[]{0x1002UL});
		public static readonly BitSet _12_in_identifier_expression241 = new BitSet(new ulong[]{0x10UL});
		public static readonly BitSet _IDENTIFIER_in_identifier_expression245 = new BitSet(new ulong[]{0x1002UL});
	}
	#endregion Follow sets
}

} // namespace SharpExpressions.parser
