<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Funcionario extends Model
{
    use HasFactory;

   protected $fillable = [
   'id',
    'id_funcionario',
    'nombre_funcionario',
    'muestra_pantalla'
   ];
}
