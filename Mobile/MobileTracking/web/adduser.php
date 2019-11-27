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
    echo "Parâmetro 'user' não foi informado.";
  }
  elseif ($password == '')
  {
    echo "Parâmetro 'password' não foi informado.";
  }
  elseif ($name == '')
  {
    echo "Parâmetro 'name' não foi informado.";
  }
  elseif ($question == '')
  {
    echo "Parâmetro 'question' não foi informado.";
  }
  elseif ($answer == '')
  {
    echo "Parâmetro 'answer' não foi informado.";
  }
  elseif ($day == '')
  {
    echo "Parâmetro 'day' não foi informado.";
  }
  elseif ($month == '')
  {
    echo "Parâmetro 'month' não foi informado.";
  }
  elseif ($year == '')
  {
    echo "Parâmetro 'year' não foi informado.";
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
               "                   'Florianópolis', current_timestamp, current_timestamp, '$question', " .
               "                   '$answer', current_timestamp) ";
               
        $result = mysql_query($sql, $link);

        if (!$result) 
        {
            echo 'ERROR: ' . mysql_error();
            $ok = false;
        }
        else
        {
          echo 'Usuário criado com sucesso.';
        }
      } 
    }/**/
    
    mysql_free_result($result);        
    mysql_close($link);
  }
?>
