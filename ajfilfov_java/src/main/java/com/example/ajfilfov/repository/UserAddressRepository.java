package com.example.ajfilfov.repository;

import com.example.ajfilfov.entity.UserAddress;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.UUID;

public interface UserAddressRepository extends JpaRepository<UserAddress, UUID> { }
