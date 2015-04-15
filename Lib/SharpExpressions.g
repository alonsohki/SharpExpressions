grammar SharpExpressions;
 
options {
    language=CSharp3;
}

@lexer::namespace{SharpExpressions.parser}
@parser::namespace{SharpExpressions.parser}

/*
 * Parser Rules
 */
 
public eval returns [Queue queue]
  : { clear_stack(); } addition_expression { queue=mQueue; }
  ;

addition_expression
  : multiply_expression
    ( '+' multiply_expression { push_operator(Operator.Add); }
	| '-' multiply_expression { push_operator(Operator.Sub); })*
  ;

multiply_expression
  : atomic_expression
    ( '*' atomic_expression { push_operator(Operator.Mul); }
	| '/' atomic_expression { push_operator(Operator.Div); })*
  ;

atomic_expression
  : factor
  | '(' addition_expression ')'
  ;

factor
  : '-' n=REAL { push_literal("-" + $n.text); }
  | n=REAL { push_literal($n.text); }
  | '-' identifier_expression { push_operator(Operator.Negate); }
  | identifier_expression
  ;

identifier_expression 
  : a=IDENTIFIER { push_identifier($a.text); } ('.' b=IDENTIFIER { push_identifier($b.text); push_operator(Operator.MemberAccess); })*
  ;
 
/*
 * Lexer Rules
 */

IDENTIFIER : ('A'..'Z' | 'a'..'z' | '_')('A'..'Z' | 'a'..'z' | '0'..'9' | '_')* ;
 
REAL : ('0'..'9')+('.' ('0'..'9')+)?
     | '.'('0'..'9')+;
WS :  (' '|'\t'|'\r'|'\n')+ {Skip();} ;
