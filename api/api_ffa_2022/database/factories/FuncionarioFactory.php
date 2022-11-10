<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;

class FuncionarioFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array
     */
    public function definition()
    {
        return [

              // 'id' => rand(1, 13),
                'id_funcionario' => rand(1, 100),
                'nombre_funcionario' => $this->faker->name(),
                'muestra_pantalla' => 0

        ];
    }
}
