package com.example.ajfilfov.repository;

import com.example.ajfilfov.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.*;

public interface UserRepository extends JpaRepository<User, UUID> {
    List<User> findByIsDeletedFalse();
}
