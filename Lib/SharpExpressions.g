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
  : { allocate_queues(5); } Q=expression { queue=$Q.ret; }
  ;

expression returns [Queue ret]
  : Q=boolean_expression { ret = $Q.ret; }
  ;

boolean_expression returns [Queue ret]
  : Q1=comparison { ret=$Q1.ret; }
    ( '&&' Q2=comparison { ret=push_operation(ret, Operator.And, $Q2.ret); }
	| '||' Q2=comparison { ret=push_operation(ret, Operator.Or,  $Q2.ret); })*
  ;

comparison returns [Queue ret]
  : Q1=addition_expression { ret=$Q1.ret; }
    ( '>=' Q2=addition_expression { ret=push_operation(ret, Operator.GreaterOrEqual, $Q2.ret); }
	| '>'  Q2=addition_expression { ret=push_operation(ret, Operator.GreaterThan, $Q2.ret); }
	| '<=' Q2=addition_expression { ret=push_operation(ret, Operator.LessOrEqual, $Q2.ret); }
	| '<'  Q2=addition_expression { ret=push_operation(ret, Operator.LessThan, $Q2.ret); }
	| '==' Q2=addition_expression { ret=push_operation(ret, Operator.Equals, $Q2.ret); }
	| '!=' Q2=addition_expression { ret=push_operation(ret, Operator.NotEquals, $Q2.ret); })*
  ;

addition_expression returns [Queue ret]
  : Q1=multiply_expression { ret=$Q1.ret; }
    ( '+' Q2=multiply_expression { ret=push_operation(ret, Operator.Add, $Q2.ret); }
	| '-' Q2=multiply_expression { ret=push_operation(ret, Operator.Sub, $Q2.ret); })*
  ;

multiply_expression returns [Queue ret]
  : Q1=power_expression { ret=$Q1.ret; }
    ( '*' Q2=power_expression { ret=push_operation(ret, Operator.Mul, $Q2.ret); }
	| '/' Q2=power_expression { ret=push_operation(ret, Operator.Div, $Q2.ret); })*
  ;

power_expression returns [Queue ret]
  : Q1=negation { ret=$Q1.ret; }
    ( '^' Q2=negation { ret=push_operation(ret, Operator.Pow, $Q2.ret); } )*
  ;

negation returns [Queue ret]
  : '-' Q=atomic_expression { ret=push_operator($Q.ret, Operator.Negate); }
  | '!' Q=atomic_expression { ret=push_operator($Q.ret, Operator.Negate); }
  | Q=atomic_expression { ret=$Q.ret; }
  ;

atomic_expression returns [Queue ret]
  : n=REAL { ret=push_literal(new_queue(), $n.text); }
  | Q=boolean_terminal { ret=$Q.ret; }
  | Q=identifier_expression { ret=$Q.ret; }
  | '(' Q=addition_expression ')' { ret=$Q.ret; }
  ;

factor returns [Queue ret]
  : '-' n=REAL { ret=push_literal(new_queue(), "-" + $n.text); }
  | n=REAL { ret=push_literal(new_queue(), $n.text); }
  | '-' Q=identifier_expression { ret=push_operator($Q.ret, Operator.Negate); }
  | Q=identifier_expression { ret=$Q.ret; }
  ;

identifier_expression returns [Queue ret]
  : a=IDENTIFIER { ret=push_identifier(new_queue(), $a.text); } ('.' b=IDENTIFIER { ret=push_operator(push_identifier(ret, $b.text), Operator.MemberAccess); })*
  ;

boolean_terminal returns [Queue ret]
  : 'true' { ret=push_boolean(new_queue(), true); }
  | 'false' { ret=push_boolean(new_queue(), false); }
  ;
 
/*
 * Lexer Rules
 */

IDENTIFIER : ('A'..'Z' | 'a'..'z' | '_')('A'..'Z' | 'a'..'z' | '0'..'9' | '_')* ;
 
REAL : ('0'..'9')+('.' ('0'..'9')+)?
     | '.'('0'..'9')+;
WS :  (' '|'\t'|'\r'|'\n')+ {Skip();} ;
