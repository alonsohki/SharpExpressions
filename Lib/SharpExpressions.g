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
  : { allocate_queues(5); } Q=addition_expression { queue=$Q.ret; }
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
  : Q1=atomic_expression { ret=$Q1.ret; }
    ( '^' Q2=atomic_expression { ret=push_operation(ret, Operator.Pow, $Q2.ret); } )*
  ;

atomic_expression returns [Queue ret]
  : Q=factor { ret=$Q.ret; }
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
 
/*
 * Lexer Rules
 */

IDENTIFIER : ('A'..'Z' | 'a'..'z' | '_')('A'..'Z' | 'a'..'z' | '0'..'9' | '_')* ;
 
REAL : ('0'..'9')+('.' ('0'..'9')+)?
     | '.'('0'..'9')+;
WS :  (' '|'\t'|'\r'|'\n')+ {Skip();} ;
