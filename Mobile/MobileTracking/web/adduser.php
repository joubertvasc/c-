<?php
  $user     = (empty($_GET['user'])) ? '' : $_GET['user'];
  $password = (empty($_GET['password'])) ? '' : $_GET['password'];
  $name     = (empty($_GET['name'])) ? '' : $_GET['name'];
  $question = (empty($_GET['question'])) ? '' : $_GET['question'];
  $answer   = (empty($_GET['answer'])) ? '' : $_GET['answer'];
  $day      = (empty($_GET['day'])) ? '' : $_GET['day'];
  $month    = (empty($_GET['month'])) ? '' : $_GET['month'];
  $year     = (empty($_GET['year'])) ? '' : $_GET['year'];

  $ok = true;

  if ($user == '')
  {
    echo "Par�metro 'user' n�o foi informado.";
  }
  elseif ($password == '')
  {
    echo "Par�metro 'password' n�o foi informado.";
  }
  elseif ($name == '')
  {
    echo "Par�metro 'name' n�o foi informado.";
  }
  elseif ($question == '')
  {
    echo "Par�metro 'question' n�o foi informado.";
  }
  elseif ($answer == '')
  {
    echo "Par�metro 'answer' n�o foi informado.";
  }
  elseif ($day == '')
  {
    echo "Par�metro 'day' n�o foi informado.";
  }
  elseif ($month == '')
  {
    echo "Par�metro 'month' n�o foi informado.";
  }
  elseif ($year == '')
  {
    echo "Par�metro 'year' n�o foi informado.";
  }
  else
  {
    $password = md5 ($password);
    
    $link = mysql_connect('dbmy0105.whservidor.com', 'jvsoftware', '45ma12mb');
    if (!$link) {
        echo 'ERROR connecting to database server: ' . mysql_error();
        $ok = false;
    }

    if ($ok)
    {
      if (!mysql_select_db('jvsoftware', $link)) 
      {
          echo 'ERROR selecting database';
          $ok = false;
      }
    
      if ($ok)
      {
        $sql = "INSERT INTO mtUsuarios (deEMail, deSenha, nmUsuario, flEmUso, idPais, deEstado, " .
               "                   deCidade, dtInclusao, dtConfirmacao, dePergunta, " .
               "                   deResposta, dtNascimento) " .
               "VALUES ('$user', '$password', '$name', 'Y', 31, 'SC', " .
               "                   'Florian�polis', current_timestamp, current_timestamp, '$question', " .
               "                   '$answer', current_timestamp) ";
               
        $result = mysql_query($sql, $link);

        if (!$result) 
        {
            echo 'ERROR: ' . mysql_error();
            $ok = false;
        }
        else
        {
          echo 'Usu�rio criado com sucesso.';
        }
      } 
    }/**/
    
    mysql_free_result($result);        
    mysql_close($link);
  }
?>
