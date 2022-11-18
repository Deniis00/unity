<?php

namespace Database\Seeders;

use App\Models\Funcionario;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        //\App\Models\User::factory(10)->create();
      //  \App\Models\Book::factory(50)->create();
        //Funcionario::factory(12)->create();
        DB::table('funcionarios')->insert([
            'id_funcionario' => 2223,
            'nombre_funcionario' => 'Carlos García',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 2245,
            'nombre_funcionario' => 'Luis Cardozo',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 2364,
            'nombre_funcionario' => 'Denis Ayala',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3429,
            'nombre_funcionario' => 'Hector Ruiz Diaz',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3434,
            'nombre_funcionario' => 'Leonardo Morinigo',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3559,
            'nombre_funcionario' => 'Liz Irrazabal',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3588,
            'nombre_funcionario' => 'Analía Rotela',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3711,
            'nombre_funcionario' => 'Liz Fernandez',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 3778,
            'nombre_funcionario' => 'Jhonny Lee',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 4808,
            'nombre_funcionario' => 'Mara Mora',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 7854,
            'nombre_funcionario' => 'Jessica Maidanaa',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 7947,
            'nombre_funcionario' => 'Miguel Paredes',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 8112,
            'nombre_funcionario' => 'Liz Molas',
            'muestra_pantalla' => 0,
        ]);

        DB::table('funcionarios')->insert([
            'id_funcionario' => 8114,
            'nombre_funcionario' => 'Facundo Franco',
            'muestra_pantalla' => 0,
        ]);

    }
}
