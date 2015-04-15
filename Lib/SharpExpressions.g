grammar SharpExpressions;
 
options {
    language=CSharp3;
}
 
@lexer::namespace{SharpExpressions.parser}
@parser::namespace{SharpExpressions.parser}

/*
 * Parser Rules
 */
 
public eval
  : addition_expression
  ;

addition_expression
  : multiply_expression
    ( '+' multiply_expression { push_operator("+"); }
	| '-' multiply_expression { push_operator("-"); })*
  ;

multiply_expression
  : atomic_expression
    ( '*' atomic_expression { push_operator("*"); }
	| '/' atomic_expression { push_operator("/"); })*
  ;

atomic_expression
  : factor
  | '(' addition_expression ')'
  ;

factor
  : '-' n=REAL { push_literal("-" + $n.text); }
  | n=REAL { push_literal($n.text); }
  | '-' identifier_expression { push_operator("negate"); }
  | identifier_expression
  ;

identifier_expression
  : n=IDENTIFIER { push_identifier($n.text); }
  | a=IDENTIFIER '.' b=IDENTIFIER { push_identifier($a.text); push_identifier($b.text); push_operator("member_access"); }
  ;
 
/*
 * Lexer Rules
 */

IDENTIFIER : ('A'..'Z' | 'a'..'z' | '_')('A'..'Z' | 'a'..'z' | '0'..'9' | '_')* ;
 
REAL : ('0'..'9')+('.' ('0'..'9')+)?
     | '.'('0'..'9')+;
WS :  (' '|'\t'|'\r'|'\n')+ {Skip();} ;
