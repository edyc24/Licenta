package com.example.ajfilfov.repository;

import com.example.ajfilfov.entity.Meciuri;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.*;

public interface MeciuriRepository extends JpaRepository<Meciuri, UUID> {
    List<Meciuri> findByIdDeletedFalse();
}
