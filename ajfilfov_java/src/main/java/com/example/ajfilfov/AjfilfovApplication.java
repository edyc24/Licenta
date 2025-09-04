package com.example.ajfilfov;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import com.example.ajfilfov.repository.MeciuriRepository;

@SpringBootApplication
@EnableJpaRepositories(basePackages = "com.example.ajfilfov.repository")
@EntityScan(basePackages = "com.example.ajfilfov.entity")
@ComponentScan(basePackages = "com.example.ajfilfov")
public class AjfilfovApplication {
    public static void main(String[] args) {
        SpringApplication.run(AjfilfovApplication.class, args);
    }
    @Bean
    CommandLineRunner testRepositories(MeciuriRepository repo) {
        return args -> System.out.println("Repo-ul Meciuri a fost injectat corect: " + repo);
    }
}
